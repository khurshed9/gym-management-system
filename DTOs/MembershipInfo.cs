using GymManagementSystem.Infrastructure.Entities.Enums;

namespace GymManagementSystem.DTOs;

public readonly record struct MembershipBaseInfo(
    MembershipType Type,
    decimal Price,
    int MaxCapacity,
    DateTime StartDate,
    DateTime EndDate);
    
    
public readonly record struct MembershipReadInfo(
    int Id,
    MembershipBaseInfo Membership);
    
    
public readonly record struct MembershipCreateInfo(
    MembershipBaseInfo Membership);
    
    
public readonly record struct MembershipUpdateInfo(
    MembershipBaseInfo Membership);