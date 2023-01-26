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

        Task init(string chatHubUri, string? tokenString);
        Task Send(Message messageInput);
        bool IsConnected { get; }
        ValueTask DisposeAsync();
    }
}
