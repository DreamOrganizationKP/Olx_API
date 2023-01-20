using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Ticket : BaseModel<string>
    {
        [Required]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
        [StringLength(4000)]
        public string Description { get; set; }
        public decimal? Price { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        public bool IsActual { get; set; }
        public virtual ICollection<TicketPhoto> Photos { get; set; }

    }
}
