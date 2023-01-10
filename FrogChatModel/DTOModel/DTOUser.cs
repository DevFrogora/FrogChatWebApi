using System.ComponentModel.DataAnnotations.Schema;

namespace FrogChatModel.DomainModel
{
    public class DTOUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
    }
}