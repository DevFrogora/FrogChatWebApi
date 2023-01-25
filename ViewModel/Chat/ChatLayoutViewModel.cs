using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Chat
{
    public class ChatLayoutViewModel :IChatLayoutViewModel
    {
        public List<UserDto> userList = new List<UserDto>();
        public List<Message> messageList = new List<Message>();

        public string _inputMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<UserDto> IChatLayoutViewModel.userList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<Message> IChatLayoutViewModel.messageList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }

        public void SendMessage()
        {
            throw new NotImplementedException();
        }
    }
}
