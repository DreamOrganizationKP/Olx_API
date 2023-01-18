using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
        

    }
}
