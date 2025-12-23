using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using AuthChallenge.Application.Services.Auth;
using AuthChallenge.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AuthSettings>(sp =>
{
    var auth = new AuthSettings();
    builder.Configuration.GetSection("AuthSettings").Bind(auth);
    return auth;
});
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IScreeningRepository, ScreeningRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetValue<string>("ConnectionString"),
        postgreOptions =>
        {
        });
    options.UseAsyncSeeding(async (db, _, cancellationToken) =>
    {
        var customer = new User() { Name = "Korosh", Username = "Korosh", PasswordHash = "" };
        db.Set<User>().AddRange();
    });
});
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudiences = builder.Configuration.GetSection("AuthSettings:ValidAudiences").Get<string[]>(),
            ValidIssuers = builder.Configuration.GetSection("AuthSettings:ValidIssuers").Get<string[]>(),
        };
    });
builder.Services.AddAuthorization(options =>
{
    foreach (var policy in AuthPolicies.All)
    {
        options.AddPolicy(policy.policyName, policy.policy);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
