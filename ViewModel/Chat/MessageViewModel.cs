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
    }
}
