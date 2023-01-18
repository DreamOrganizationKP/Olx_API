namespace Data.Models
{
    public interface IModel<TId>
    {
        public TId Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public abstract class BaseModel<TId>: IModel<TId>
    {
        public TId Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
