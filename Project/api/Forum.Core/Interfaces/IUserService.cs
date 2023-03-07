using Forum.Core.Aggregates.User.Entities;


namespace Forum.Core.Interfaces;

public interface IUserService
{
    Task<ICollection<UserEntity>> GetUserList();
    Task<UserEntity?> GetUserById(Guid id);
    Task<UserEntity?> GetUserByLogin(string login);
    Task<UserEntity> CreateUser(UserEntity request);
    Task<UserEntity> UpdateUser(Guid id, UserEntity request);
    Task<UserEntity> UpdateProfilePicture(Guid id, UserEntity request);
    Task DeleteUser(Guid id);
    Task HardDeleteUser(Guid id);
}
