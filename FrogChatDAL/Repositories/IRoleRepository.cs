using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories
{
    public interface IRoleRepository
    {
        Task<TblRole> GetRoleAsync(int id);
        Task<IEnumerable<TblRole>> GetRolesAsync();
    }
}
