using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;

namespace GymManagementSystem.Infrastructure.Services.MembershipService;

public interface IMembershipService
{
    Task<Result<PaginationResponse<IEnumerable<MembershipReadInfo>>>> GetMembershipsAsync(MembershipFilter filter);
    
    Task<Result<MembershipReadInfo>> GetMembershipByIdAsync(int id);
    
    Task<BaseResult> CreateMembershipAsync(MembershipCreateInfo createInfo);
    
    Task<BaseResult> UpdateMembershipAsync(int id, MembershipUpdateInfo updateInfo);
    
    Task<BaseResult> DeleteMembershipAsync(int id);
}