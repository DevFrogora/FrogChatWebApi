﻿using FrogChatModel.ChatModel;
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
        List<ChatUser> userList { get; set; }
        List<Message> messageList { get; set; }
        string _inputMessage { get; set; }
        Task DisconnectAsync();
        void SendMessage(Message message);
        event Action NotifyToUIOnMessageDelete;
        event Action NotifyToUIUpdate;
        Task init(string chatHubUri, string? tokenString);

    }
}
