﻿using FrogChatModel.ChatModel;
using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FrogChatWebApi.Hubs
{
    public static class UserHandler
    {
        public static Dictionary<string,int> UserCount = new Dictionary<string,int>();
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub :Hub
    {
        private readonly ChatPersistenceData chatPersistenceData;

        public ChatHub(ChatPersistenceData chatPersistenceData)
        {
            this.chatPersistenceData = chatPersistenceData;
        }

        public async Task SendMessage(Message message)
        {
            message.id = chatPersistenceData.MessageId;
            message.dateTime = DateTime.UtcNow;
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.User.Claims.Where(claim => claim.Type == "userId").Select(claim => claim.Value).FirstOrDefault();
            UserHandler.UserCount.TryGetValue(userId, out var founduser);
            if (founduser == 0)
            {
                UserHandler.UserCount.Add(userId,1);
                UserDto userDto = new UserDto()
                {
                    Name = Context.User.Identity.Name,
                    Email = Context.User.Claims.Where(claim => claim.Type == ClaimTypes.Email).Select(claim => claim.Value).FirstOrDefault(),
                    PhotoUrl = Context.User.Claims.Where(claim => claim.Type == "picture").Select(claim => claim.Value).FirstOrDefault(),
                    Username = Context.User.Claims.Where(claim => claim.Type == "username").Select(claim => claim.Value).FirstOrDefault(),
                    Id = userId,
                };
                chatPersistenceData.users.Add(userDto);
                //Console.WriteLine(Context.User.Identity.Name);
            }
            else
            {
                UserHandler.UserCount[userId] = ++founduser;
            }
            await Clients.All.SendAsync("Connected", chatPersistenceData.users);

            //return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string Id = Context.User.Claims.Where(claim => claim.Type == "userId").Select(claim => claim.Value).FirstOrDefault();
            UserHandler.UserCount.TryGetValue(Id, out var founduser);
            if(founduser == 1)
            {
                chatPersistenceData.users.Remove(chatPersistenceData.users.Where(user => user.Id.Equals(Id)).FirstOrDefault());
                UserHandler.UserCount.Remove(Id);
            }
            else
            {
                UserHandler.UserCount[Id] = --founduser;

            }
            await Clients.All.SendAsync("Disconnected", chatPersistenceData.users);
            //return base.OnDisconnectedAsync(exception);
        }
    }
}
