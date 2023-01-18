using Data.Repositories.Classes;

namespace Services.ServiceClasses
{
    public class UserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository)
        {
            _repository = repository;
        }
    }
}
