using KoiPondConstruct.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    //LOGIN ONLY
    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthService() { }
        public AuthService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //TODO: decode password from DB -> compare with input password
        public bool Login(string username, string password)
        {
            return true;
        }

    }
}
