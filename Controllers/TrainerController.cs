using BestPracticeExceptionHandler.Extensions.PatternResultExtensions;
using GymManagementSystem.DTOs;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Services.TrainerService;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeExceptionHandler.Controllers;

[Route("/api/trainers")]
public class TrainerController : BaseController
{
    private readonly ITrainerService trainerService;

    public TrainerController(ITrainerService trainerService)
    {
        this.trainerService = trainerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrainers([FromQuery] TrainerFilter filter)
        => (await trainerService.GetTrainersAsync(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTrainerById(int id)
        => (await trainerService.GetTrainerByIdAsync(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> CreateTrainer([FromBody] TrainerCreateInfo info)
        => (await trainerService.CreateTrainerAsync(info)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTrainer(int id, [FromBody] TrainerUpdateInfo info)
        => (await trainerService.UpdateTrainerAsync(id, info)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTrainer(int id)
        => (await trainerService.DeleteTrainerAsync(id)).ToActionResult();
}
