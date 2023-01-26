using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FrogChatWebApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub :Hub
    {
        private readonly ChatPersistenceData chatPersistenceData;

        public ChatHub(ChatPersistenceData chatPersistenceData)
        {
            this.chatPersistenceData = chatPersistenceData;
        }

        public async Task SendMessage(Message message)
        {
            message.id = chatPersistenceData.MessageId;
            message.dateTime = DateTime.UtcNow;
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            UserDto userDto = new UserDto()
            {
                Name = Context.User.Identity.Name,
                Email = Context.User.Claims.Where(claim => claim.Type == ClaimTypes.Email).Select(claim => claim.Value).FirstOrDefault(),
                PhotoUrl = Context.User.Claims.Where(claim => claim.Type == "picture").Select(claim => claim.Value).FirstOrDefault(),
                Username = Context.User.Claims.Where(claim => claim.Type == "username").Select(claim => claim.Value).FirstOrDefault(),
                Id = Context.User.Claims.Where(claim => claim.Type == "userId").Select(claim => claim.Value).FirstOrDefault(),
            };
            chatPersistenceData.users.Add(userDto);
            //Console.WriteLine(Context.User.Identity.Name);
            await Clients.All.SendAsync("Connected", chatPersistenceData.users);

            //return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string Id = Context.User.Claims.Where(claim => claim.Type == "userId").Select(claim => claim.Value).FirstOrDefault();
            chatPersistenceData.users.Remove(chatPersistenceData.users.Where(user => user.Id.Equals(Id)).FirstOrDefault());
            await Clients.All.SendAsync("Disconnected", chatPersistenceData.users);
            //return base.OnDisconnectedAsync(exception);
        }
    }
}
