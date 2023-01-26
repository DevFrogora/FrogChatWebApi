using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using FrogChatService.ChatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Chat
{
    public class ChatLayoutViewModel :IChatLayoutViewModel
    {
        private readonly IChatService chatService;
        public List<UserDto> userList { get; set; } = new();
        public List<Message> messageList { get; set; } = new();

        public ChatLayoutViewModel(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public event Action OnMessageReceivedDelegate;

        public string _inputMessage { get ; set; }

        public async Task init(string chatHubUri, string? tokenString)
        {
            chatService.OnMessageReceivedPublisher += OnMessageReceived;
            await chatService.init(chatHubUri, tokenString);
        }
        void OnMessageReceived(Message message)
        {
            messageList.Add(message);
            OnMessageReceivedDelegate();
        }
        

        public async Task DisconnectAsync()
        {
           await chatService.DisposeAsync();
        }

        public void SendMessage(Message newMessage)
        {
            chatService.Send(newMessage);
        }
    }
}
