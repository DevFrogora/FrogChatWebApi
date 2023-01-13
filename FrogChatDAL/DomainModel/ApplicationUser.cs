using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.DomainModel
{
    public class ApplicationUser :IdentityUser
    {
        public string PhotoUrl { get; set; }

        public string Name { get; set; }

    }
}
