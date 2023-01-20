using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Model;

namespace ViewModel
{
    public interface IContactsViewModel
    {
        public List<Contact> Contacts { get; set; }
        public Task GetContacts();
    }
}
