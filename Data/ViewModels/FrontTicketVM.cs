using Data.Models;

namespace Data.ViewModels
{
    public class FrontTicketVM
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserId { get; set; }
        public FrontUserVM User { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public decimal? Price { get; set; }

        public string Location { get; set; }

        public virtual ICollection<FrontTicketPhotoVM> Photos { get; set; }
    }
}
