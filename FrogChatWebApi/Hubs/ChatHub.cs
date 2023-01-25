using FrogChatModel.ChatModel;
using Microsoft.AspNetCore.SignalR;

namespace FrogChatWebApi.Hubs
{
    public class ChatHub :Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
