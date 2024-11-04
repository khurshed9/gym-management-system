namespace GymManagementSystem.DTOs;

public readonly record struct TrainerBaseInfo(
    string FullName,
    string Specialization,
    DateTime DateOfBirth,
    string PhoneNumber);
    
 
public readonly record struct TrainerReadInfo(
    int Id,
    TrainerBaseInfo TrainerBaseInfo);
    
 
public readonly record struct TrainerCreateInfo(
    TrainerBaseInfo TrainerBaseInfo);
    
    
public readonly record struct TrainerUpdateInfo(
    TrainerBaseInfo TrainerBaseInfo);