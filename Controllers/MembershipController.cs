using BestPracticeExceptionHandler.Extensions.PatternResultExtensions;
using GymManagementSystem.DTOs;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Services.MembershipService;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeExceptionHandler.Controllers;

[Route("/api/memberships")]
public class MembershipController : BaseController
{
    private readonly IMembershipService membershipService;

    public MembershipController(IMembershipService membershipService)
    {
        this.membershipService = membershipService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberships([FromQuery] MembershipFilter filter)
        => (await membershipService.GetMembershipsAsync(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMembershipById(int id)
        => (await membershipService.GetMembershipByIdAsync(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> CreateMembership([FromBody] MembershipCreateInfo info)
        => (await membershipService.CreateMembershipAsync(info)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMembership(int id, [FromBody] MembershipUpdateInfo info)
        => (await membershipService.UpdateMembershipAsync(id, info)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMembership(int id)
        => (await membershipService.DeleteMembershipAsync(id)).ToActionResult();
}
