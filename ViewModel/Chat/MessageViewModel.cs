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

        public MessageViewModel()
        {

        }

        public MessageViewModel(IClipboardService clipboardService)
        {
            this.clipboardService = clipboardService;
        }

        public Message message {get; set;}

        public void CopyId()
        {
            clipboardService.CopyToClipboard(message.id.ToString());
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }

        public static implicit operator MessageViewModel(Message meessage)
        {

            return new MessageViewModel
            {
                message = meessage
            };
        }
    }
}
