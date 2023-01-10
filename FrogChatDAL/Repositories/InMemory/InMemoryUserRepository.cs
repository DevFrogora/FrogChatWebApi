using FrogChatModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.InMemory
{
    public class InMemoryUserRepository : IUserRepository
    {
        private List<TblUser> Users = new List<TblUser>()
        {
            new TblUser(){
                Id= 1,
                Name = "Frogora",
                Email = "rr4428310@gmail.com",
                Identifier = "115412363636721186069",
                PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5r5h2endXrYUH1Ad9moiIPDxZy6bYoI5ppRO8mqg=s96-c",
                RoleId = 1
            },
            new TblUser
            {
                Id=2,
                Name = "nanu naka",
                Email = "my4lol78695@gmail.com",
                Identifier = "109111229606383522361",
                PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c",
                RoleId = 3
            }
        };

        public async Task<TblUser> AddUserAsync(TblUser user)
        {
            Users.Add(user);
            return await Task.FromResult(user);
        }

        public Task<TblUser> DeleteUserAsync(string id)
        {
            var result = Users.First(user => user.Identifier.Equals(id));
            Users.Remove(result);

            return Task.FromResult(result);
        }

        public Task<TblUser> GetUserAsync(string id)
        {
            return Task.FromResult(Users.First(user => user.Identifier.Equals(id)));
        }

        public async Task<IEnumerable<TblUser>> GetUsersAsync()
        {
            return await Task.FromResult(Users);
        }

        public async Task<TblUser> UpdateUserAsync(TblUser user)
        {
            var result = Users.First(u => u.Identifier == user.Identifier);
            result.Name = user.Name;
            result.Email = user.Email;
            result.PhotoPath = user.PhotoPath;
            result.RoleId = user.RoleId;

            return await Task.FromResult(result); ;
        }
    }
}
