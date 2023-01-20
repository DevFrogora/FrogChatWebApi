using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.DTOModel
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        
        //public string Identifier { get; set; }
        //public string 

        public static implicit operator UserDto(SignUpUserDto user)
        {

            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                PhotoUrl = user.PhotoPath,
            };
        }

    }
}
