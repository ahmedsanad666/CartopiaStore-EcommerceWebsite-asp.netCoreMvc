using System.ComponentModel.DataAnnotations;

namespace CartopiaStore.Models
{
    public class Auth
    {
        [Required(ErrorMessage = "Email address is required.")]
      
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }
    }

}
