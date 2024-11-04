using GymManagementSystem.DTOs;
using GymManagementSystem.Infrastructure.Entities;

namespace GymManagementSystem.Extensions.Mapper;

public static class TrainerMappingExtensions
{
    public static TrainerReadInfo ToReadInfo(this Trainer trainer)
    {
        return new()
        {
            Id = trainer.Id,
            TrainerBaseInfo = new()
            {
                FullName = trainer.FullName,
                Specialization = trainer.Specialization,
                DateOfBirth = trainer.DateOfBirth,
                PhoneNumber = trainer.PhoneNumber,
            }
        };
    }

    public static Trainer ToTrainer(this TrainerCreateInfo createInfo)
    {
        return new()
        {
            FullName = createInfo.TrainerBaseInfo.FullName,
            Specialization = createInfo.TrainerBaseInfo.Specialization,
            DateOfBirth = createInfo.TrainerBaseInfo.DateOfBirth,
            PhoneNumber = createInfo.TrainerBaseInfo.PhoneNumber
        };
    }

    public static Trainer ToUpdateTrainer(this Trainer trainer, TrainerUpdateInfo updateInfo)
    {
        trainer.FullName = updateInfo.TrainerBaseInfo.FullName;
        trainer.Specialization = updateInfo.TrainerBaseInfo.Specialization;
        trainer.DateOfBirth = updateInfo.TrainerBaseInfo.DateOfBirth;
        trainer.PhoneNumber = updateInfo.TrainerBaseInfo.PhoneNumber;
        trainer.Version++;
        trainer.UpdatedAt = DateTime.UtcNow;
        return trainer;
    }

    public static Trainer ToDeletedTrainer(this Trainer trainer)
    {
        trainer.IsDeleted = true;
        trainer.DeletedAt = DateTime.UtcNow;
        trainer.UpdatedAt = DateTime.UtcNow;
        trainer.Version++;
        return trainer;
    }
}