using KoiPondConstruct.Data.Base;
using KoiPondConstruct.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Data.Repository.Impl
{
    public class UserRepository : BaseRepository<TblUser>
    {
        public UserRepository() { }

        public UserRepository(FA24_SE1702_PRN221_G2_KoiPondConstructContext context) : base(context)
        {
            _context = context;
        }
        public async Task<TblUser> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.TblUsers.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}
