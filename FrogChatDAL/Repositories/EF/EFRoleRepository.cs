using FrogChatModel.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.EF
{
    public class EFRoleRepository : IRoleRepository
    {
        private readonly FrogChatDbContext frogChatDbContext;

        public EFRoleRepository(FrogChatDbContext frogChatDbContext)
        {
            this.frogChatDbContext = frogChatDbContext;
        }

        public async Task<TblRole> GetRoleAsync(int roleId)
        {
            return await frogChatDbContext.Roles
                .FirstAsync(r => r.Id == roleId);
        }


        public async Task<IEnumerable<TblRole>> GetRolesAsync()
        {
            return await frogChatDbContext.Roles.ToListAsync();
        }
    }
}
