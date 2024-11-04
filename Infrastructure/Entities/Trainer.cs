namespace GymManagementSystem.Infrastructure.Entities;

public class Trainer : BaseEntity
{
    public string FullName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
    
    public string PhoneNumber { get; set; } = null!;

    public ICollection<Schedule> Schedules { get; set; } = [];
}