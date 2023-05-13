using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Requests
{
    public class RegisterRequest
    {
        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{6,20}$", ErrorMessage = "User name should be 6-20 characters long with alphnumerics and _")]
        public string? username { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_!+=?]{10,24}$", ErrorMessage = "Password should be 10-24 characters long with alphnumerics and _!+=?")]
        public string? password { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_!+=?]{6,20}$", ErrorMessage = "Alias should be 6-20 characters long with alphnumerics and _!+=?")]
        public string? alias { get; set; }
    }
}
