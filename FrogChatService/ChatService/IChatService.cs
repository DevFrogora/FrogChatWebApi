using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatService.ChatService
{

    public interface IChatService
    {
        event Action<Message> OnMessageReceivedPublisher;
        event Action<List<UserDto>> OnUserListReceivedPublisher;
        event Action<int> OnMessageDelete;
        event Action<Message> OnMessageEdit;


        Task init(string chatHubUri, string? tokenString);
        Task Send(Message messageInput);
        Task Delete_Message(int messageId);
        Task Edit_Message(Message message);
        bool IsConnected { get; }
        ValueTask DisposeAsync();
    }
}
