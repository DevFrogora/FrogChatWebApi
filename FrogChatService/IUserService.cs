using FrogChatModel.DomainModel;

namespace FrogChatService
{
    public interface IUserService
    {
        Task<IEnumerable<DTOUser>> GetUsersAsync();

    }
}