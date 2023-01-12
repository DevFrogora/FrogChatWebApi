using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL
{
    public class FrogChatDbContext : IdentityDbContext
    {
        public FrogChatDbContext(DbContextOptions<FrogChatDbContext> options)
            : base(options)
        {

        }

    }
}
