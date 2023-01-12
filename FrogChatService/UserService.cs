using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrogChatService
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SignUpUserDto> GetUserAsync(string identifier)
        {
            return await httpClient.GetFromJsonAsync<SignUpUserDto>($"api/user/{identifier}") ?? throw new Exception();
        }

        public async Task<IEnumerable<SignUpUserDto>> GetUsersAsync()
        {
            return await httpClient.GetFromJsonAsync<SignUpUserDto[]>("api/user") ?? throw new Exception();
            //return  await httpClient.GetFromJsonAsync<List<DTOUser>>("api/user") ?? throw new Exception();
        }

        public async Task<SignUpUserDto> AddUserAsync(SignUpUserDto updatedUser)
        {
            return await httpClient.PostJsonAsync<SignUpUserDto>("api/user", updatedUser) ?? throw new Exception();
        }

        public async Task<SignUpUserDto> UpdateUserAsync(SignUpUserDto updatedUser)
        {
            return await httpClient.PutJsonAsync<SignUpUserDto>("api/user", updatedUser) ?? throw new Exception();
        }

        public async Task<SignUpUserDto> DeleteUserAsync(SignUpUserDto updatedUser)
        {
            return await httpClient.DeletetJsonAsync<SignUpUserDto>($"api/user/{updatedUser.Identifier}") ?? throw new Exception();
        }
    }
}
