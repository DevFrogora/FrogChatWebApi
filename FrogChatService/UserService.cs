using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<IEnumerable<DTOUser>> GetUsersAsync()
        {
            return  await httpClient.GetFromJsonAsync<List<DTOUser>>("api/user") ?? throw new Exception();
             
        }
    }
}
