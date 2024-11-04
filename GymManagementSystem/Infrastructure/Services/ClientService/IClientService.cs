using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;

namespace GymManagementSystem.Infrastructure.Services.ClientService;

public interface IClientService
{
    Task<Result<PaginationResponse<IEnumerable<ClientReadInfo>>>> GetClientsAsync(ClientFilter filter);
    
    Task<Result<ClientReadInfo>> GetClientByIdAsync(int id);
    
    Task<BaseResult> CreateClientAsync(ClientCreateInfo createInfo);
    
    Task<BaseResult> UpdateClientAsync(int id, ClientUpdateInfo updateInfo);
    
    Task<BaseResult> DeleteClientAsync(int id);
}