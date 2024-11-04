using GymManagementSystem.Infrastructure.Entities.Enums;

namespace GymManagementSystem.Infrastructure.Entities;

public class Membership : BaseEntity
{
    public MembershipType Type { get; set; }

    public decimal Price { get; set; }

    public int MaxCapacity { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ICollection<Client> Clients { get; set; } = [];
}