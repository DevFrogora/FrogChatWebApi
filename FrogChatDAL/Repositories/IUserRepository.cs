using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories
{
    public interface IUserRepository
    {

        Task<TblUser> AddUserAsync(TblUser user);
        Task<TblUser> GetUserAsync(string id);
        Task<IEnumerable<TblUser>> GetUsersAsync();
        Task<TblUser> UpdateUserAsync(TblUser user);
        Task<TblUser> DeleteUserAsync(string id);
    }
}
