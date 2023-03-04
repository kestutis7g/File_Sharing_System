using Forum.Core.Aggregates.User.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Core.Aggregates.User.Specs;
using Microsoft.EntityFrameworkCore;

namespace Forum.Core.Services;

public class UserService : IUserService
{
    public UserService(IRepository<UserEntity> userRepo)
    {
        UserRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
    }
    private IRepository<UserEntity> UserRepo { get; }


    public async Task<ICollection<UserEntity>> GetUserList()
    {
        return await UserRepo.ListAsync();
    }

    public async Task<UserEntity?> GetUserById(Guid id)
    {
        return await UserRepo.GetByIdAsync(id);
    }

    public async Task<UserEntity?> GetUserByLogin(string login)
    {
        var spec = new GetUserByLoginSpec(login);
        return await UserRepo.GetBySpecAsync(spec);
    }

    public async Task<UserEntity> CreateUser(UserEntity request)
    {
        return await UserRepo.AddAsync(request);
    }

    public async Task<UserEntity> UpdateUser(Guid id, UserEntity request)
    {
        var user = await GetUserById(id);
        if(user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        user.Update(request);
        await UserRepo.SaveChangesAsync();
        return user;

    }

    public async Task DeleteUser(Guid id)
    {
        var user = await GetUserById(id);
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        user.MarkDeleted();
        await UserRepo.SaveChangesAsync();

    }

    public async Task HardDeleteUser(Guid id)
    {
        var user = await GetUserById(id);
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        await UserRepo.DeleteAsync(user);

    }
}
