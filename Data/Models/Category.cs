using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Category : BaseModel<string>
    {
        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        public string Photo { get; set; }
        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> SubCategories { get; set;}
        //public virtual ICollection<Ticket> Tickets { get; set;}

    }
}
