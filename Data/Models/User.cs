using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Surname { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Ticket> UserTickets { get; set; }
        
    }
}
