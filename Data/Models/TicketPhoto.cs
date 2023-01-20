using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class TicketPhoto : BaseModel<string>
    {
        [Required]
        public string Path { get; set; }
        public int Index { get; set; }

        [Required]
        [ForeignKey("Ticket")]
        public string TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
