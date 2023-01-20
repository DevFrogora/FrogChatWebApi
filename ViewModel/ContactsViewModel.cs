using FrogChatModel.DTOModel;
using FrogChatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Model;

namespace ViewModel
{
    public class ContactsViewModel :IContactsViewModel
    {


        //properties
        public List<Contact> Contacts { get; set; }
        private readonly IUserService userService;

        //methods
        public ContactsViewModel()
        {
        }
        public ContactsViewModel(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task GetContacts()
        {
            var users = await userService.GetUsersAsync();
            LoadCurrentObject(users.ToList<UserDto>());
        }

        private void LoadCurrentObject(List<UserDto> users)
        {
            this.Contacts = new List<Contact>();
            foreach (UserDto user in users)
            {
                this.Contacts.Add(user);
            }
        }

    }
}
