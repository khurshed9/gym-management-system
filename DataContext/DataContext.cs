using GymManagementSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DataContext;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }

    public DbSet<Trainer> Trainers { get; set; }

    public DbSet<Membership> Memberships { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

}