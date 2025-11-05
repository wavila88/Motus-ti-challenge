using Domain.DTO;

namespace Domain.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetUserList();
        Task SaveUser(UserSaveDto user);

        Task<UserDto> GetUserById(int userId);
        Task<UserDto> GetUserByEmail(string email);

        Task UpdateUser(UserSaveDto user);

        Task DeleteUser(int userId);
    }
}
