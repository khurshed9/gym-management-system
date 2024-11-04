namespace GymManagementSystem.DTOs;

public readonly record struct ScheduleBaseInfo(
    DateTime StartTime,
    DateTime EndTime,
    int TrainerId);
    
    
public readonly record struct ScheduleReadInfo(
    int Id,
    ScheduleBaseInfo Schedule);
    
    
public readonly record struct ScheduleCreateInfo(
    ScheduleBaseInfo Schedule);
    
    
public readonly record struct ScheduleUpdateInfo(
    ScheduleBaseInfo Schedule);