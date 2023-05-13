using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Requests
{
    public class RenameRequest
    {
        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_!+=?]{6,20}$", ErrorMessage = "Land alias should be 6-20 characters long with alphnumerics and _!+=?")]
        public string? Alias { get; set; }
        public int LandId { get; set; }
    }
}
