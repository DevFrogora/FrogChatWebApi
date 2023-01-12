using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace FrogChatModel.DomainModel
{
    public class SignUpUserDto
    {
        [Required(ErrorMessage = "Please Enter Your Google Identifier")]
        [Display(Name = "Identifier")]
        [StringLength(21, MinimumLength = 21, ErrorMessage = "Identifier size is incorrect")]
        public string Identifier { get; set; } 
        
        [Required(ErrorMessage = "Please Enter your Username")]
        [Display(Name = "UserName")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "Please Enter your Direct Url of your Pic")]
        [Display(Name = "Photo URL")]
        [UrlValidAttribute]
        public string PhotoPath { get; set; } 
    }
    public class UrlValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string url = (string)value;
            if (string.IsNullOrEmpty(url))
                return true;

            HttpWebResponse res;
            try
            {
                Uri uri = new Uri(url, UriKind.Absolute);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                res = (HttpWebResponse)req.GetResponse();
            }
            catch
            {
                return false;
            }

            switch (res.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectKeepVerb:
                case HttpStatusCode.RedirectMethod:
                    return true;
            }

            return false;
        }
    }
}
