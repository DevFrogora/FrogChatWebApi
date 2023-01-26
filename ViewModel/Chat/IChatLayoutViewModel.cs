using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Chat
{
    public interface IChatLayoutViewModel
    {
        List<UserDto> userList { get; set; }
        List<Message> messageList { get; set; }
        string _inputMessage { get; set; }
        Task DisconnectAsync();
        void SendMessage(Message message);
        event Action OnMessageReceivedDelegate;
        Task init(string chatHubUri);

    }
}
