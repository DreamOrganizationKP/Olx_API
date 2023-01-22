namespace Data.ViewModels
{
    public class CreateTicketRequestVM
    {
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Location { get; set; }
    }
}
