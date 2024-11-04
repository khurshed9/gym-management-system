using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.Mapper;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Services.MembershipService;

public class MembershipService(DataContext.DataContext context) : IMembershipService
{

    public async Task<Result<PaginationResponse<IEnumerable<MembershipReadInfo>>>> GetMembershipsAsync(MembershipFilter filter)
    {
        IQueryable<Membership> memberships = context.Memberships;

        if (filter.Type.HasValue)
        {
            memberships = memberships.Where(m => m.Type == filter.Type.Value);
        }

        int totalRecords = await memberships.CountAsync();

        IQueryable<MembershipReadInfo> result = memberships
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.ToReadInfo());

        PaginationResponse<IEnumerable<MembershipReadInfo>> response = PaginationResponse<IEnumerable<MembershipReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, totalRecords, result);

        return Result<PaginationResponse<IEnumerable<MembershipReadInfo>>>.Success(response);
    }

    public async Task<Result<MembershipReadInfo>> GetMembershipByIdAsync(int id)
    {
        Membership? membership = await context.Memberships.FirstOrDefaultAsync(m => m.Id == id);

        return membership is null
           ? Result<MembershipReadInfo>.Failure(Error.NotFound())
           : Result<MembershipReadInfo>.Success(membership.ToReadInfo()); 
    }

    public async Task<BaseResult> CreateMembershipAsync(MembershipCreateInfo createInfo)
    {
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;

        bool hasMembershipThisMonth = await context.Memberships
            .AnyAsync(m => m.Clients.Any(c => c.Id == m.Id) &&  
                           m.StartDate.Month == currentMonth &&
                           m.StartDate.Year == currentYear);
        
        if (hasMembershipThisMonth)
            return BaseResult.Failure(Error.AlreadyExist("Client can only have one membership per month."));

        
        bool conflict = await context.Memberships.AnyAsync(x => x.Type == createInfo.Membership.Type);
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());

        await context.Memberships.AddAsync(createInfo.ToMembership()); 
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateMembershipAsync(int id, MembershipUpdateInfo updateInfo)
    {
        Membership? existingMembership = await context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
        if (existingMembership is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = await context.Memberships.AnyAsync(x
            => x.Id != id && x.Type == updateInfo.Membership.Type);

        if (conflict)
            return BaseResult.Failure(Error.Conflict());

        existingMembership.ToUpdateMembership(updateInfo); 
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteMembershipAsync(int id)
    {
        Membership? membership = await context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
        if (membership is null)
            return BaseResult.Failure(Error.NotFound());

        context.Memberships.Remove(membership);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted!!!"))
            : BaseResult.Success();
    }
}
