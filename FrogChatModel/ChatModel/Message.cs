using FrogChatModel.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.ChatModel
{
    public class Message
    {
        public int id { get; set; }
        public string content { get; set; }
        public DateTime dateTime { get; set; }
        public UserDto user { get; set; }
        public bool IsNotice => content.StartsWith("[Notice]");
        //public bool Mine { get; set; }
    }
}
