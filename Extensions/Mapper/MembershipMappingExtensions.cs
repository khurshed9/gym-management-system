using GymManagementSystem.DTOs;
using GymManagementSystem.Infrastructure.Entities;

namespace GymManagementSystem.Extensions.Mapper;

public static class MembershipMappingExtensions
{
    public static MembershipReadInfo ToReadInfo(this Membership membership)
    {
        return new ()
        {
            Id = membership.Id,
            Membership = new ()
            {
                Type = membership.Type,
                Price = membership.Price,
                MaxCapacity = membership.MaxCapacity,
                StartDate = membership.StartDate,
                EndDate = membership.EndDate
            }
        };
    }

    public static Membership ToMembership(this MembershipCreateInfo membership)
    {
        return new ()
        {
            Type = membership.Membership.Type,
            Price = membership.Membership.Price,
            MaxCapacity = membership.Membership.MaxCapacity,
            StartDate = membership.Membership.StartDate,
            EndDate = membership.Membership.EndDate
        };
    }

    public static Membership ToUpdateMembership(this Membership membership, MembershipUpdateInfo updateInfo)
    {
        membership.Type = updateInfo.Membership.Type;
        membership.Price = updateInfo.Membership.Price;
        membership.MaxCapacity = updateInfo.Membership.MaxCapacity;
        membership.StartDate = updateInfo.Membership.StartDate;
        membership.EndDate = updateInfo.Membership.EndDate;
        membership.Version++;
        membership.UpdatedAt = DateTime.UtcNow;
        return membership;
    }

    public static Membership ToDeletedMembership(this Membership membership)
    {
        membership.IsDeleted = true;
        membership.DeletedAt = DateTime.UtcNow;
        membership.UpdatedAt = DateTime.UtcNow;
        membership.Version++;
        return membership;
    }
}