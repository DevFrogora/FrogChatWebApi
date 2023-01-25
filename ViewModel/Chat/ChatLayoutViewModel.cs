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


        public string _inputMessage { get ; set; }

        public async Task init()
        {
            chatService.OnMessageReceivedPublisher += OnMessageReceived;
            await chatService.init();
        }
        void OnMessageReceived(Message message)
        {
            messageList.Add(message);
        }
        

        public async Task DisconnectAsync()
        {
           await chatService.DisposeAsync();
        }

        public void SendMessage()
        {
            chatService.Send(_inputMessage);
        }
    }
}
