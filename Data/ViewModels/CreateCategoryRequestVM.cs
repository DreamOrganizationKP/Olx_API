namespace Data.ViewModels
{
    public class CreateCategoryRequestVM
    {
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
