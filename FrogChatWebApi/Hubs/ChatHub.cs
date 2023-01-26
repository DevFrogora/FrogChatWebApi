using FrogChatModel.ChatModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

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

        public override Task OnConnectedAsync()
        {
            //chatPersistenceData.users.Add();
            Console.WriteLine(Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
    }
}
