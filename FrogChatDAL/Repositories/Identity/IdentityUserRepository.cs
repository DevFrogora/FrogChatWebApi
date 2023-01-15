﻿using AutoMapper;
using FrogChatDAL.DomainModel;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL.Repositories.Identity
{
    public class IdentityUserRepository :IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public IdentityUserRepository(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return userManager.Users.ToList();
        }

        public async Task<IdentityResult> UpdateUser(SignUpUserDto signUpUserDto )
        {
            var user = await  userManager.FindByEmailAsync(signUpUserDto.Email);
            if (user == null)
            {
                return null;
            }
            else
            {
                user.Name = signUpUserDto.Name;
                user.PhotoUrl = signUpUserDto.PhotoPath;
                return await userManager.UpdateAsync(user);
            }
        }

        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return null;
            }
            return await userManager.DeleteAsync(user);
        }
    }
}