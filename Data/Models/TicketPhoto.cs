namespace Data.Models
{
    public class TicketPhoto : BaseModel<string>
    {
        public string Path { get; set; }
        public int Index { get; set; }
    }
}
