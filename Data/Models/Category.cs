namespace Data.Models
{
    public class Category : BaseModel<string>
    {
        public string Name { get; set; }

        public string Photo { get; set; }
        public ICollection<Category> SubCategories { get; set;}
    }
}
