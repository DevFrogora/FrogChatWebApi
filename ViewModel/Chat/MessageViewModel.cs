using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using FrogChatService.ChatService;
using FrogChatService.ClipBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Chat
{
    public class MessageViewModel : IMessageViewModel
    {
        private readonly IClipboardService clipboardService;
        private readonly IChatService chatService;

        public bool isEditing { get; set; } = false;
        public string tempEditingMsg { get; set; }
        private bool IsEditingToogle
        {
            get
            {
                isEditing = !isEditing;
                return isEditing;
            }
        }

        public MessageViewModel(IClipboardService clipboardService,IChatService chatService)
        {
            this.clipboardService = clipboardService;
            this.chatService = chatService;
        }

        public Message message {get; set;}

        public void CopyId()
        {
            Console.WriteLine(message.content);
            clipboardService.CopyToClipboard(message.id.ToString());
        }

        public void Delete()
        {
            Console.WriteLine(message.id);
            chatService.Delete_Message(message.id);
        }

        public void Edit()
        {
            chatService.Edit_Message(message);
        }

        public void EditbtnClick()
        {
            tempEditingMsg = message.content;
            _ = IsEditingToogle;
        }
        public void EditCancel()
        {
            tempEditingMsg = message.content;
            _ = IsEditingToogle;
        }
        public void EditSave()
        {
            message.content = tempEditingMsg;
            Edit();
            _ = IsEditingToogle;
        }

        public string userNameColor { get; set; }
        public void HeighestRole()
        {
            if (message.user.HeighestRole == 4)
            {
                userNameColor = "color:red;";
            }
            if (message.user.HeighestRole == 3)
            {
                userNameColor = "color:orangered;";
            }
            if (message.user.HeighestRole == 2)
            {
                userNameColor = "color:cornflowerblue;";
            }
            if (message.user.HeighestRole == 1)
            {
                userNameColor = "color:whitesmoke;";
            }
        }
    }
}
