using FrogChatModel.ChatModel;
using Microsoft.AspNetCore.SignalR;

namespace FrogChatWebApi.Hubs
{
    public class ChatHub :Hub
    {
        int i=0;
        public async Task SendMessage(Message message)
        {
            message.id = ++i;
            message.dateTime = DateTime.UtcNow;
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
