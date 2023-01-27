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

        public event Action NotifyToUIUpdate;
        public event Action NotifyToUIOnMessageDelete;


        public string _inputMessage { get ; set; }

        public async Task init(string chatHubUri, string? tokenString)
        {
            chatService.OnMessageReceivedPublisher += OnMessageReceived;
            chatService.OnUserListReceivedPublisher += ChatService_OnUserListReceivedPublisher;
            chatService.OnMessageDelete += ChatService_OnMessageDelete;
            await chatService.init(chatHubUri, tokenString);
        }

        private void ChatService_OnMessageDelete(int messageId)
        {
            var mesageToDelete = messageList.Where(message => message.id == messageId).FirstOrDefault();
            if (mesageToDelete != null)
            {
                Console.WriteLine($"Deleted ID: {mesageToDelete.id} Content: {mesageToDelete.content} ");

                messageList.Remove(mesageToDelete);
                NotifyToUIOnMessageDelete();
            }
        }

        private void ChatService_OnUserListReceivedPublisher(List<UserDto> _userList)
        {
            userList = _userList;
            NotifyToUIUpdate();
        }

        void OnMessageReceived(Message message)
        {
            messageList.Add(message);
            NotifyToUIUpdate();
        }
        

        public async Task DisconnectAsync()
        {
           await chatService.DisposeAsync();
        }

        public void SendMessage(Message newMessage)
        {
            chatService.Send(newMessage);
            _inputMessage = string.Empty;
        }
    }
}
