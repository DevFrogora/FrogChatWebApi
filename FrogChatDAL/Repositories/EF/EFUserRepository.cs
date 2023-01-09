using FrogChatModel.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.EF
{
    public class EFUserRepository : IUserRepository
    {
        private readonly FrogChatDbContext frogChatDbContext;

        public EFUserRepository(FrogChatDbContext frogChatDbContext)
        {
            this.frogChatDbContext = frogChatDbContext;
        }

        public async Task<TblUser?> AddUserAsync(TblUser user)
        {
            var result = await frogChatDbContext.Users.AddAsync(user);
            await frogChatDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TblUser?> DeleteUserAsync(string identifier)
        {
            var result = await frogChatDbContext.Users
                .FirstOrDefaultAsync(u => u.Identifier == identifier);
            if (result != null)
            {
                frogChatDbContext.Users.Remove(result);
                await frogChatDbContext.SaveChangesAsync();
                return result;
            }
            return result;
        }

        public async Task<TblUser?> GetUserAsync(string identifier)
        {
            return await frogChatDbContext.Users
               .FirstAsync(u => u.Identifier == identifier);
        }

        public async Task<IEnumerable<TblUser?>> GetUsersAsync()
        {
            return await frogChatDbContext.Users.ToListAsync();
        }

        public async Task<TblUser?> UpdateUserAsync(TblUser user)
        {
            var result = await frogChatDbContext.Users
                .FirstOrDefaultAsync(u => u.Identifier == user.Identifier);
            if (result != null)
            {
                result.Name = user.Name;
                result.Email = user.Email;
                result.PhotoPath = user.PhotoPath;
                result.RoleId = user.RoleId;

                await frogChatDbContext.SaveChangesAsync();

                return result;
            }
            return result;
        }
    }
}
