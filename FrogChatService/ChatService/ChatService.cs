using FrogChatModel.ChatModel;
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

        //private List<Message> messages = new List<Message>();

        //public delegate void OnMessageReceived(Message message);
        //public event OnMessageReceived OnMessageReceivedPublisher;

        public  async Task init(string chatHubUri,string? tokenString)
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
