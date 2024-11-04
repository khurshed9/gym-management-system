using GymManagementSystem.DTOs;
using GymManagementSystem.Infrastructure.Entities;

namespace GymManagementSystem.Extensions.Mapper;

public static class ScheduleMappingExtensions
{
    public static ScheduleReadInfo ToReadInfo(this Schedule schedule)
    {
        return new()
        {
            Id = schedule.Id,
            Schedule = new()
            {
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                TrainerId = schedule.TrainerId
            }
        };
    }

    public static Schedule ToSchedule(this ScheduleCreateInfo createInfo)
    {
        return new()
        {
            StartTime = createInfo.Schedule.StartTime,
            EndTime = createInfo.Schedule.EndTime,
            TrainerId = createInfo.Schedule.TrainerId
        };
    }

    public static Schedule ToUpdateSchedule(this Schedule schedule, ScheduleUpdateInfo updateInfo)
    {
        schedule.StartTime = updateInfo.Schedule.StartTime;
        schedule.EndTime = updateInfo.Schedule.EndTime;
        schedule.TrainerId = updateInfo.Schedule.TrainerId;
        schedule.Version++;
        schedule.UpdatedAt = DateTime.UtcNow;
        return schedule;
    }

    public static Schedule ToDeletedSchedule(this Schedule schedule)
    {
        schedule.IsDeleted = true;
        schedule.DeletedAt = DateTime.UtcNow;
        schedule.UpdatedAt = DateTime.UtcNow;
        schedule.Version++;
        return schedule;
    }
}