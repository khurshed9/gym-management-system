using GymManagementSystem.Infrastructure.Entities.Enums;

namespace GymManagementSystem.Filter;

public record MembershipFilter(MembershipType? Type) : BaseFilter;