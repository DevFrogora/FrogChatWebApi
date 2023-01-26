using FrogChatModel.ChatModel;
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
        Task init(string chatHubUri);
        Task Send(Message messageInput);
        bool IsConnected { get; }
        ValueTask DisposeAsync();
    }
}
