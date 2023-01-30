using AutoMapper;
using Data.Models;
using Data.Repositories.Classes;
using Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Services.ServiceClasses
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;
        private readonly JwtTokenService _jwtTokenService;
        public UserService(UserRepository repository, IMapper mapper, JwtTokenService jwtTokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RegisterResponseVM> RegisterAsync(RegisterRequestVM model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                
                var result = await _repository.RegisterAsync(user, model.Password);

                if(result.Succeeded)
                {
                    var profile = _mapper.Map<UserProfileVM>(user);
                    var token = await _jwtTokenService.CreateTokenAsync(user);

                    return new RegisterResponseVM()
                    {
                        IsSuccess = true,
                        Profile = profile,
                        AccessToken = token
                    };
                }

                return new RegisterResponseVM()
                {
                    IsSuccess = false,
                };
            }
            catch (Exception)
            {
                return new RegisterResponseVM()
                {
                    IsSuccess = false,
                };
            }
        }
        public async Task<LoginResponseVM> LoginAsync(LoginRequestVM model)
        {
            try
            {
                var result = await _repository.LoginAsync(model.Login, model.Password, model.IsRemember);

                if (result.Succeeded)
                {
                    var user = await _repository.GetUserByEmail(model.Login);
                    var profile = _mapper.Map<UserProfileVM>(user);

                    var token = await _jwtTokenService.CreateTokenAsync(user);
                    return new LoginResponseVM()
                    {
                        IsSuccess = true,
                        IsCredentialsValid = true,
                        Profile = profile,
                        AccessToken = token
                    };
                }

                return new LoginResponseVM()
                {
                    IsSuccess = false,
                };

            }
            catch (Exception)
            {
                return new LoginResponseVM()
                {
                    IsSuccess = false,
                };
            }
        }

        public async Task<SimpleResponseVM> ExternalGoogleLoginAsync(string googleToken)
        {
            try
            {
                var validationResult = await _jwtTokenService.VerifyGoogleTokenAsync(googleToken);

                if (validationResult == null)
                {
                    return new SimpleResponseVM()
                    {
                        IsSuccess = false
                    };
                }

                var info = new UserLoginInfo("Google", validationResult.Subject, "Google");
                var user = await _repository.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if(user == null)
                {
                    user = await _repository.GetUserByEmail(validationResult.Email);

                    if(user == null)
                    {
                        user = _mapper.Map<User>(validationResult);
                        var registerResult = await _repository.RegisterAsync(user);

                        if (!registerResult.Succeeded)
                        {
                            return new SimpleResponseVM()
                            {
                                IsSuccess = false,
                            };
                        }
                    }
                    var result = await _repository.AddLoginAsync(user, info);

                    if(!result.Succeeded)
                    {
                        return new SimpleResponseVM()
                        {
                            IsSuccess = false,
                        };
                    }
                }

                var accessToken = await _jwtTokenService.CreateTokenAsync(user);
                return new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = accessToken
                };
            }
            catch (Exception)
            {
                return new SimpleResponseVM()
                {
                    IsSuccess = false
                };
            }
        }
    }
}