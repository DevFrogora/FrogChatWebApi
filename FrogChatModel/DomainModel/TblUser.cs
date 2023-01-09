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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string PhotoPath { get; set; }

        public int RoleId { get; set; }
        public virtual TblRole Role { get; set; }
    }
}
