using FrogChatModel.DTOModel;
using FrogChatService;

namespace ViewModel
{
    public interface IProfileViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public string profileServiceStatus { get; set; }
        public Task UpdateProfile();
        public Task GetProfile();

    }
}