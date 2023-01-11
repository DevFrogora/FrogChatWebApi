﻿using FrogChatModel.DomainModel;

namespace FrogChatService
{
    public interface IUserService
    {
        Task<IEnumerable<DTOUser>> GetUsersAsync();
        Task<DTOUser> GetUser(string identifier);
        Task<DTOUser> UpdateUser(DTOUser updatedUser);
    }
}