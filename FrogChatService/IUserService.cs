using FrogChatModel.DomainModel;

namespace FrogChatService
{
    public interface IUserService
    {
        Task<IEnumerable<SignUpUserDto>> GetUsersAsync();
        Task<SignUpUserDto> GetUserAsync(string identifier);
        Task<SignUpUserDto> UpdateUserAsync(SignUpUserDto updatedUser);
        Task<SignUpUserDto> AddUserAsync(SignUpUserDto user);
        Task<SignUpUserDto> DeleteUserAsync(SignUpUserDto updatedUser);
    }
}