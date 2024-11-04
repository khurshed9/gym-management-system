using GymManagementSystem.DTOs;
using GymManagementSystem.Infrastructure.Entities;

namespace GymManagementSystem.Extensions.Mapper;

public static class ClientMappingExtensions
{
    public static ClientReadInfo ToReadInfo(this Client client)
    {
        return new()
        {
            Id = client.Id,
            ClientBaseInfo = new()
            {
                FullName = client.FullName,
                PhoneNumber = client.PhoneNumber,
                Age = client.Age,
                MembershipId = client.MembershipId,
                ScheduleId = client.ScheduleId
            }
        };
    }

    public static Client ToClient(this ClientCreateInfo createInfo)
    {
        return new()
        {
            FullName = createInfo.ClientBaseInfo.FullName,
            PhoneNumber = createInfo.ClientBaseInfo.PhoneNumber,
            Age = createInfo.ClientBaseInfo.Age,
            MembershipId = createInfo.ClientBaseInfo.MembershipId,
            ScheduleId = createInfo.ClientBaseInfo.ScheduleId
        };
    }

    public static Client ToUpdateClient(this Client client ,ClientUpdateInfo updateInfo)
    {
        client.FullName = updateInfo.ClientBaseInfo.FullName;
        client.PhoneNumber = updateInfo.ClientBaseInfo.PhoneNumber;
        client.Age = updateInfo.ClientBaseInfo.Age;
        client.MembershipId = updateInfo.ClientBaseInfo.MembershipId;
        client.ScheduleId = updateInfo.ClientBaseInfo.ScheduleId;
        client.Version++;
        client.UpdatedAt = DateTime.UtcNow;
        return client;
    }

    public static Client ToDeletedClient(this Client client)
    {
        client.IsDeleted = true;
        client.DeletedAt = DateTime.UtcNow;
        client.UpdatedAt = DateTime.UtcNow;
        client.Version++;
        return client;
    }
}