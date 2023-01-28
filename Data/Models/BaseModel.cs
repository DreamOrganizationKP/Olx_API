using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public interface IModel<TId>
    {
        public TId Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public abstract class BaseModel<TId>: IModel<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
