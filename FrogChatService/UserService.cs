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

        public async Task<DTOUser> GetUserAsync(string identifier)
        {
            return await httpClient.GetFromJsonAsync<DTOUser>($"api/user/{identifier}") ?? throw new Exception();
        }

        public async Task<IEnumerable<DTOUser>> GetUsersAsync()
        {
            return await httpClient.GetFromJsonAsync<DTOUser[]>("api/user") ?? throw new Exception();
            //return  await httpClient.GetFromJsonAsync<List<DTOUser>>("api/user") ?? throw new Exception();
        }

        public async Task<DTOUser> AddUserAsync(DTOUser updatedUser)
        {
            return await httpClient.PostJsonAsync<DTOUser>("api/user", updatedUser) ?? throw new Exception();
        }

        public async Task<DTOUser> UpdateUserAsync(DTOUser updatedUser)
        {
            return await httpClient.PutJsonAsync<DTOUser>("api/user", updatedUser) ?? throw new Exception();
        }

        public async Task<DTOUser> DeleteUserAsync(DTOUser updatedUser)
        {
            return await httpClient.DeletetJsonAsync<DTOUser>($"api/user/{updatedUser.Identifier}") ?? throw new Exception();
        }
    }
}
