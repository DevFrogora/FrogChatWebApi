using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.DomainModel
{
    public class TblUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public virtual TblRole? Role { get; set; }
    }
}
