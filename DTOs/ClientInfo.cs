namespace GymManagementSystem.DTOs;

public readonly record struct ClientBaseInfo(
    string FullName,
    string PhoneNumber,
    int Age,
    int MembershipId,
    int ScheduleId);
    
    
public readonly record struct ClientReadInfo(
    int Id,
    ClientBaseInfo ClientBaseInfo);
    
    
public readonly record struct ClientCreateInfo(
    ClientBaseInfo ClientBaseInfo);
    
    
public readonly record struct ClientUpdateInfo(
    ClientBaseInfo ClientBaseInfo);