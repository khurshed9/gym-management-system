using BestPracticeExceptionHandler.Extensions.PatternResultExtensions;
using GymManagementSystem.DTOs;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Services.ScheduleService;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeExceptionHandler.Controllers;

[Route("/api/schedules")]
public class ScheduleController : BaseController
{
    private readonly IScheduleService scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        this.scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedules([FromQuery] ScheduleFilter filter)
        => (await scheduleService.GetSchedulesAsync(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetScheduleById(int id)
        => (await scheduleService.GetScheduleByIdAsync(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] ScheduleCreateInfo info)
        => (await scheduleService.CreateScheduleAsync(info)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleUpdateInfo info)
        => (await scheduleService.UpdateScheduleAsync(id, info)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSchedule(int id)
        => (await scheduleService.DeleteScheduleAsync(id)).ToActionResult();
}
