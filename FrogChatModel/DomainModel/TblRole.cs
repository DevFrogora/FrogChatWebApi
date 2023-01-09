using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.DomainModel
{
    public class TblRole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TblUser> Users { get; set; }
    }
}
