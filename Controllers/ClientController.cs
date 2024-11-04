using BestPracticeExceptionHandler.Extensions.PatternResultExtensions;
using GymManagementSystem.DTOs;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Services.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeExceptionHandler.Controllers;

[Route("/api/clients")]
public class ClientController(IClientService clientService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetClients([FromQuery] ClientFilter filter)
        => (await clientService.GetClientsAsync(filter)).ToActionResult();


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClientById(int id)
        => (await clientService.GetClientByIdAsync(id)).ToActionResult();


    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientCreateInfo info)
        => (await clientService.CreateClientAsync(info)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient(int id,[FromBody] ClientUpdateInfo info)
        => (await clientService.UpdateClientAsync(id,info)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClient(int id)
        => (await clientService.DeleteClientAsync(id)).ToActionResult();
}