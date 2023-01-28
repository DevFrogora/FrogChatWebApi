﻿using FrogChatModel.ChatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Chat
{
    public interface IMessageViewModel
    {
        Message message { get; set; }
        void Delete();
        void CopyId();
        void Edit();
        void EditbtnClick();
        void EditCancel();
        void EditSave();
        string tempEditingMsg { get; set; }
        bool isEditing { get; set; }
        string userNameColor { get; set; }
        void HeighestRole();
    }
}
