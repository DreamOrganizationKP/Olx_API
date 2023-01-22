namespace Data.ViewModels
{
    public class RegisterResponseVM : BaseResponseVM
    {
        public UserProfileVM Profile { get; set; }
        public string AccessToken { get; set; }
    }
}
