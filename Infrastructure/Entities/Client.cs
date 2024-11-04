namespace GymManagementSystem.Infrastructure.Entities;

public class Client : BaseEntity
{
    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int Age { get; set; }


    public int MembershipId { get; set; }
    public Membership Membership { get; set; } = null!;

    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = null!;
}