namespace Data.Models
{
    public class Ticket : BaseModel<string>
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public bool IsActual { get; set; }
        public ICollection<TicketPhoto> Photos { get; set; }

    }
}
