using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Data.Repository.Impl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KoiPondConstruct.Service
{
    // LOGIN ONLY
    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.UserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // TODO: decode password from DB -> compare with input password
        public async Task<bool> LoginAsync(string username, string password)
        {
            TblUser checkedUser = await _userRepository.GetUserByUsernameAndPasswordAsync(username, password);
            if (checkedUser == null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("isAuth", "false");
                return false;
            }

            // Set username & isAuthenticated into session web
            int userIdTemp = (int)checkedUser.Id;
            _httpContextAccessor.HttpContext.Session.SetString("username", checkedUser.Username);
            _httpContextAccessor.HttpContext.Session.SetString("role", checkedUser.Role);
            _httpContextAccessor.HttpContext.Session.SetInt32("userid", userIdTemp);


            _httpContextAccessor.HttpContext.Session.SetString("isAuth", "true");
            return true;
        }
    }
}
