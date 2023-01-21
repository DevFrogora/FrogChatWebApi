using FrogChatModel.DTOModel;
using FrogChatService;

namespace ViewModel
{
    public class ProfileViewModel : IProfileViewModel
    {
        private readonly IUserService userService;

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public string profileServiceStatus { get; set; }

        public ProfileViewModel()
        {

        }

        public ProfileViewModel(IUserService userService)
        {
            this.userService = userService;

        }

        public async Task UpdateProfile()
        {   UserDto user = this;
            var update = await userService.UpdateUserAsync(user);
            if (update.IsSuccessStatusCode)
            {
                this.profileServiceStatus = "profile updated Successfully " + update.StatusCode;
                userDto = this;
            }
            else
            {
                this.profileServiceStatus = "Not updated : " + update.StatusCode;
            }
        }
        UserDto userDto;
        public async Task GetProfile()
        {
            if (userDto == null)
            {
               userDto = await userService.GetUserProfileAsync();
            }
            LoadCurrentObject(userDto);
            this.profileServiceStatus = "profile Get Successfully";
        }


        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            this.UserId = profileViewModel.UserId;
            this.UserName = profileViewModel.UserName;
            this.Name = profileViewModel.Name;
            this.EmailAddress = profileViewModel.EmailAddress;
            this.PhotoUrl = profileViewModel.PhotoUrl;
        }

        public static implicit operator ProfileViewModel(UserDto user)
        {

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