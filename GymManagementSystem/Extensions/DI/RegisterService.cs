using GymManagementSystem.Infrastructure.Services.ClientService;
using GymManagementSystem.Infrastructure.Services.MembershipService;
using GymManagementSystem.Infrastructure.Services.ScheduleService;
using GymManagementSystem.Infrastructure.Services.TrainerService;

namespace GymManagementSystem.Extensions.DI;

public static class RegisterService
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<ITrainerService, TrainerService>();
        services.AddTransient<IMembershipService, MembershipService>();
        services.AddTransient<IScheduleService, ScheduleService>();
        return services;
    }
}