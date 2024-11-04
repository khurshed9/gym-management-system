using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.Mapper;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Services.ScheduleService;

public class ScheduleService(DataContext.DataContext context) : IScheduleService
{

    public async Task<Result<PaginationResponse<IEnumerable<ScheduleReadInfo>>>> GetSchedulesAsync(ScheduleFilter filter)
    {
        IQueryable<Schedule> schedules = context.Schedules;
        
        if(filter.StartTime != null)
            schedules = schedules.Where(s => s.StartTime >= filter.StartTime);

        int totalRecords = await schedules.CountAsync();

        IQueryable<ScheduleReadInfo> result = schedules
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.ToReadInfo());

        PaginationResponse<IEnumerable<ScheduleReadInfo>> response = PaginationResponse<IEnumerable<ScheduleReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, totalRecords, result);

        return Result<PaginationResponse<IEnumerable<ScheduleReadInfo>>>.Success(response);
    }


    public async Task<Result<ScheduleReadInfo>> GetScheduleByIdAsync(int id)
    {
        Schedule? schedule = await context.Schedules
            .Include(s => s.Trainer) 
            .FirstOrDefaultAsync(s => s.Id == id);

        return schedule is null
            ? Result<ScheduleReadInfo>.Failure(Error.NotFound())
            : Result<ScheduleReadInfo>.Success(schedule.ToReadInfo());
    }

    public async Task<BaseResult> CreateScheduleAsync(ScheduleCreateInfo createInfo)
    {
        bool conflict = await context.Schedules.AnyAsync(s => s.StartTime == createInfo.Schedule.StartTime);
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist("Schedule conflict detected."));

        await context.Schedules.AddAsync(createInfo.ToSchedule());
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateScheduleAsync(int id, ScheduleUpdateInfo updateInfo)
    {
        Schedule? existingSchedule = await context.Schedules.FirstOrDefaultAsync(x => x.Id == id);
        if (existingSchedule is null)
            return BaseResult.Failure(Error.NotFound());

        existingSchedule.ToUpdateSchedule(updateInfo);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteScheduleAsync(int id)
    {
        Schedule? schedule = await context.Schedules.FirstOrDefaultAsync(x => x.Id == id);
        if (schedule is null)
            return BaseResult.Failure(Error.NotFound());

        context.Schedules.Remove(schedule);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted!"))
            : BaseResult.Success();
    }
}
