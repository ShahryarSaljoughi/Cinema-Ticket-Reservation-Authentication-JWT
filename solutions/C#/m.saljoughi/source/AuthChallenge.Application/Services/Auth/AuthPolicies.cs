using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AuthChallenge.Application.Services.Auth;

public class AuthPolicies
{
    public const string ScreenWriterAdmin = nameof(ScreenWriterAdmin);
    public const string Admin = nameof(Admin);

    public static (string policyName, AuthorizationPolicy policy)[] All { get; set; }
    static AuthPolicies()
    {
        var permissionBasedPolicies = new List<AuthorizationPolicy>();
        var adminPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole("Admin")
            .Build();
        permissionBasedPolicies.Add(adminPolicy);

        var screenWriterAdmin = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole("Admin")
            .RequireAssertion(context =>
            {
                var scopeClaim = context.User.FindFirst("scope");
                if (scopeClaim == null) return false;
                var scopes = scopeClaim.Value.Split(" ", StringSplitOptions.TrimEntries);
                return scopes.Any(s => s == "screenings:write");
            })
            .Build();


        All = [
            (ScreenWriterAdmin, screenWriterAdmin),
            (Admin, adminPolicy)
        ];
    }
}
