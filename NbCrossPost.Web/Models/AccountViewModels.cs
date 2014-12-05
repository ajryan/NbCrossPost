using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NbCrossPost.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
