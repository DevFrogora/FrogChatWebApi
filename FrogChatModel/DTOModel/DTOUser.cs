using System.ComponentModel.DataAnnotations.Schema;

namespace FrogChatModel.DomainModel
{
    public class DTOUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string PhotoPath { get; set; }
    }
}