using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
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



        public MessageViewModel(IClipboardService clipboardService)
        {
            this.clipboardService = clipboardService;
        }

        //public MessageViewModel()
        //{

        //}

        public Message message {get; set;}

        public void CopyId()
        {
            Console.WriteLine(message.content);
            clipboardService.CopyToClipboard(message.id.ToString());
        }

        public void Delete()
        {
        }

        public void Edit()
        {
        }

        //public static implicit operator MessageViewModel(Message meessage)
        //{

        //    return new MessageViewModel
        //    {
        //        message = meessage
        //    };
        //}
    }
}
