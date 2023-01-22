namespace Data.ViewModels
{
    public class LoginResponseVM : BaseResponseVM
    {
        public bool IsCredentialsValid { get; set; }
        public UserProfileVM Profile { get; set; }
        public string AccessToken { get; set; }

    }
}
