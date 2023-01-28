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
    public class ChatService : IChatService
    {
        private HubConnection? hubConnection;

        public event Action<Message> OnMessageReceivedPublisher;
        public event Action<List<ChatUser>> OnUserListReceivedPublisher;
        public event Action<int> OnMessageDelete;
        public event Action<Message> OnMessageEdit;



        //private List<Message> messages = new List<Message>();

        //public delegate void OnMessageReceived(Message message);
        //public event OnMessageReceived OnMessageReceivedPublisher;

        public async Task init(string chatHubUri,string? tokenString)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri(chatHubUri), options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(tokenString);
                })  // server uri <-
                .Build();

            hubConnection.On<Message>("ReceiveMessage", ( message) =>
            {
                //messages.Add(message);
                //InvokeAsync(StateHasChanged);
                OnMessageReceivedPublisher(message);
            });

            hubConnection.On<int>("ReceiveDeleteMessage", (messageId) =>
            {
                OnMessageDelete(messageId);
            });

            hubConnection.On<Message>("ReceiveEditMessage", (message) =>
            {
                OnMessageEdit(message);
            });


            hubConnection.On<List<ChatUser>>("Connected", (userList) =>
            {
                OnUserListReceivedPublisher(userList);
            });

            hubConnection.On<List<ChatUser>>("Disconnected", (userList) =>
            {
                OnUserListReceivedPublisher(userList);
            });

            //hubConnection
            await hubConnection.StartAsync();
        }

        public async Task Send(Message messageInput)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", messageInput);
            }
        }

        public async Task Edit_Message(Message message)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("EditMessage", message);
            }
        }

        public async Task Delete_Message(int messageId)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("DeleteMessage", messageId);
            }
        }

        public bool IsConnected =>
            hubConnection?.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
