using GymManagementSystem.Infrastructure.Entities;

namespace GymManagementSystem.Filter;

public record TrainerFilter(string? Specialization) : BaseFilter;