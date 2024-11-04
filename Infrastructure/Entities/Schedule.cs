namespace GymManagementSystem.Infrastructure.Entities;

public class Schedule : BaseEntity
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public ICollection<Client> Clients { get; set; } = [];
}