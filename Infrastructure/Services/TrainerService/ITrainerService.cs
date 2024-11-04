using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;

namespace GymManagementSystem.Infrastructure.Services.TrainerService;

public interface ITrainerService
{
    Task<Result<PaginationResponse<IEnumerable<TrainerReadInfo>>>> GetTrainersAsync(TrainerFilter filter);
    
    Task<Result<TrainerReadInfo>> GetTrainerByIdAsync(int id);
    
    Task<BaseResult> CreateTrainerAsync(TrainerCreateInfo createInfo);
    
    Task<BaseResult> UpdateTrainerAsync(int id, TrainerUpdateInfo updateInfo);
    
    Task<BaseResult> DeleteTrainerAsync(int id);
}