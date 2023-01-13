using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.DTOModel
{
    public class SignInDto
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Identifier { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
