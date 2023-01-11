using FrogChatModel.DomainModel;

namespace FrogChatService
{
    public interface IUserService
    {
        Task<IEnumerable<DTOUser>> GetUsersAsync();
        Task<DTOUser> GetUserAsync(string identifier);
        Task<DTOUser> UpdateUserAsync(DTOUser updatedUser);
        Task<DTOUser> AddUserAsync(DTOUser user);
        Task<DTOUser> DeleteUserAsync(DTOUser updatedUser);
    }
}