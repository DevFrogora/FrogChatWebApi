using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.ChatModel
{
    public class ChatUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        
        public List<ChatRole> Roles { get; set; }
        public int HeighestRole { get; set; }

    }
}
