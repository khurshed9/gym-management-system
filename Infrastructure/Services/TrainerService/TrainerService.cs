using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.Mapper;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Services.TrainerService;

public class TrainerService(DataContext.DataContext context) : ITrainerService
{

    public async Task<Result<PaginationResponse<IEnumerable<TrainerReadInfo>>>> GetTrainersAsync(TrainerFilter filter)
    {
        IQueryable<Trainer> trainers = context.Trainers;

        if (!string.IsNullOrWhiteSpace(filter.Specialization))
        {
            trainers = trainers.Where(t => t.Specialization.ToLower() == filter.Specialization.ToLower());
        }

        int totalRecords = await trainers.CountAsync();

        IQueryable<TrainerReadInfo> result = trainers
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.ToReadInfo()); 

        PaginationResponse<IEnumerable<TrainerReadInfo>> response = PaginationResponse<IEnumerable<TrainerReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, totalRecords, result);

        return Result<PaginationResponse<IEnumerable<TrainerReadInfo>>>.Success(response);
    }

    public async Task<Result<TrainerReadInfo>> GetTrainerByIdAsync(int id)
    {
        Trainer? trainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == id);

        return trainer is null
           ? Result<TrainerReadInfo>.Failure(Error.NotFound())
           : Result<TrainerReadInfo>.Success(trainer.ToReadInfo()); 
    }

    public async Task<BaseResult> CreateTrainerAsync(TrainerCreateInfo createInfo)
    {
        bool conflict = await context.Trainers.AnyAsync(x => x.FullName.ToLower() == createInfo.TrainerBaseInfo.FullName.ToLower());
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());

        await context.Trainers.AddAsync(createInfo.ToTrainer()); 
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateTrainerAsync(int id, TrainerUpdateInfo updateInfo)
    {
        Trainer? existingTrainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
        if (existingTrainer is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = await context.Trainers.AnyAsync(x
            => x.Id != id && x.FullName.ToLower() == updateInfo.TrainerBaseInfo.FullName.ToLower());

        if (conflict)
            return BaseResult.Failure(Error.Conflict());

        existingTrainer.ToUpdateTrainer(updateInfo); 
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteTrainerAsync(int id)
    {
        Trainer? trainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
        if (trainer is null)
            return BaseResult.Failure(Error.NotFound());

        context.Trainers.Remove(trainer);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted!!!"))
            : BaseResult.Success();
    }
}
