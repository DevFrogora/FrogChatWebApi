using FrogChatModel.DTOModel;

namespace ViewModel
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public string updateStatus { get; set; }

        public static implicit operator ProfileViewModel(UserDto user) {

            return new ProfileViewModel
            {
                UserId = user.Id,
                UserName = user.Username,
                Name = user.Name,
                EmailAddress = user.Email,
                PhotoUrl = user.PhotoUrl,
            };
        }
        public static implicit operator UserDto(ProfileViewModel user)
        {

            return new UserDto
            {
                Id = user.UserId,
                Username = user.UserName,
                Name = user.Name,
                Email = user.EmailAddress,
                PhotoUrl = user.PhotoUrl,
            };
        }

    }
}