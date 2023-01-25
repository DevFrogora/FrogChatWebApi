using FrogChatModel.ChatModel;
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
    }
}
