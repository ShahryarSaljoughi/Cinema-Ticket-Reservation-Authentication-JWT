using AuthChallenge.Api.ApiModels;
using AuthChallenge.Api.ApiModels.DTOs;
using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using AuthChallenge.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace AuthChallenge.Api.Controllers;

[Authorize()]
[ApiController]
public class ScreeningsController: ControllerBase
{
    public ScreeningsController(IScreeningRepository screeningRepo, IUnitOfWork unitOfWork)
    {
        ScreeningRepo = screeningRepo;
        UnitOfWork = unitOfWork;
    }

    public IScreeningRepository ScreeningRepo { get; }
    public IUnitOfWork UnitOfWork { get; }


    [Authorize(Policy = AuthPolicies.ScreenWriterAdmin)]
    [HttpPost, Route("api/[controller]")]
    public async Task<ActionResult<AddScreeningResponse>> AddAsync(AddScreeningRequest request, CancellationToken cancellation)
    {
        var screening = new Screening 
        { 
            MovieTitle = request.MovieTitle, 
            StartsAt = DateTimeOffset.UtcNow,
            Hall = request.Hall
        };
        await ScreeningRepo.AddAsync(screening);
        await UnitOfWork.SaveChangesAsync(cancellation);
        return Ok(new AddScreeningResponse(screening.Id));
    }

    [AllowAnonymous]
    [HttpGet, Route("api/[controller]")]
    public async Task<ActionResult<ScreeningDto[]>> GetAll()
    {
        var screenings = await ScreeningRepo.GetAllAsync();
        return Ok(screenings ?? []);
    }
}
