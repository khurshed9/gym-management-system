using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;

namespace GymManagementSystem.Infrastructure.Services.ScheduleService;

public interface IScheduleService
{
    Task<Result<PaginationResponse<IEnumerable<ScheduleReadInfo>>>> GetSchedulesAsync(ScheduleFilter filter);
    
    Task<Result<ScheduleReadInfo>> GetScheduleByIdAsync(int id);
    
    Task<BaseResult> CreateScheduleAsync(ScheduleCreateInfo createInfo);
    
    Task<BaseResult> UpdateScheduleAsync(int id, ScheduleUpdateInfo updateInfo);
    
    Task<BaseResult> DeleteScheduleAsync(int id);
}