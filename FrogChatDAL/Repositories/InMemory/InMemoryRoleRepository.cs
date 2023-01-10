using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.InMemory
{
    public class InMemoryRoleRepository : IRoleRepository
    {
        List<TblRole> roles = new List<TblRole>
        {
            new TblRole { Id = 1, Name = "Admin" },
            new TblRole { Id = 2, Name = "Manager" },
            new TblRole { Id = 3, Name = "User" }
        };
        public Task<TblRole> GetRoleAsync(int id)
        {
            return Task.FromResult(
                roles.First(role => role.Id == id));
        }

        public async Task<IEnumerable<TblRole>> GetRolesAsync()
        {
            return await Task.FromResult(roles);
        }
    }
}
