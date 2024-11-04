using System.Runtime.InteropServices.JavaScript;
using BestPracticeExceptionHandler.Responses;
using GymManagementSystem.DTOs;
using GymManagementSystem.Extensions.Mapper;
using GymManagementSystem.Extensions.PatternResultExtensions;
using GymManagementSystem.Filter;
using GymManagementSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Services.ClientService;

public class ClientService(DataContext.DataContext context) : IClientService
{
    public async Task<Result<PaginationResponse<IEnumerable<ClientReadInfo>>>> GetClientsAsync(ClientFilter filter)
    {
        IQueryable<Client> clients = context.Clients;

        if (filter.Age > 0)
            clients = clients.Where(c => c.Age == filter.Age);
        
        int totalRecords = await clients.CountAsync();

        IQueryable<ClientReadInfo> result = clients
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).Select(x => x.ToReadInfo());

        PaginationResponse<IEnumerable<ClientReadInfo>> response = PaginationResponse<IEnumerable<ClientReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, totalRecords, result);

        return Result<PaginationResponse<IEnumerable<ClientReadInfo>>>.Success(response);
    }

    public async Task<Result<ClientReadInfo>> GetClientByIdAsync(int id)
    {
        Client? client = await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

        return client is null
           ? Result<ClientReadInfo>.Failure(Error.NotFound())
            : Result<ClientReadInfo>.Success(client.ToReadInfo());
    }

    public async Task<BaseResult> CreateClientAsync(ClientCreateInfo createInfo)
    {
        bool conflict = await context.Clients.AnyAsync(x => x.FullName.ToLower() == createInfo.ClientBaseInfo.FullName.ToLower());
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());
        await context.Clients.AddAsync(createInfo.ToClient());
        int res = await context.SaveChangesAsync();
        
        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved !!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateClientAsync(int id, ClientUpdateInfo updateInfo)
    {
        Client? existingClient = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        if (existingClient is null)
            return BaseResult.Failure(Error.NotFound());
        
        bool conflict = await context.Clients.AnyAsync(x
            => x.Id != id && x.FullName.ToLower() ==
            updateInfo.ClientBaseInfo.FullName.ToLower());
        
        if (conflict)
            return BaseResult.Failure(Error.Conflict());
        
        existingClient.ToUpdateClient(updateInfo);
        int res = await context.SaveChangesAsync();
        
        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!!!"))
             : BaseResult.Success();
        

    }

    public async Task<BaseResult> DeleteClientAsync(int id)
    {
        Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        if (client is null)
            return BaseResult.Failure(Error.NotFound());
        
        context.Clients.Remove(client);
        int res = await context.SaveChangesAsync();
        
        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted!!!"))
             : BaseResult.Success();
    }
}