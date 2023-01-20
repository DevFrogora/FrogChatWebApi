using FrogChatModel.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Model
{
    public class Contact
    {
        public Contact()
        {

        }

        public Contact(string Name, string Id)
        {
            this.Name = Name;
            this.Id = Id;
        }
        public string Name { get; set; }
        public string Id { get; set; }

        public static implicit operator Contact(UserDto user)
        {

            return new Contact
            {
                Name = user.Name,
                Id = user.Id,

            };
        }
    }
}
