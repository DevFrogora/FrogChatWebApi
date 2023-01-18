using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;

namespace FrogChatService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<SignUpUserDto> GetUserAsync(string identifier);
        Task<HttpResponseMessage> UpdateUserAsync(UserDto updatedUser);
        Task<SignUpUserDto> AddUserAsync(SignUpUserDto user);
        Task<SignUpUserDto> DeleteUserAsync(SignUpUserDto updatedUser);
    }
}