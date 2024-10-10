using KoiPondConstruct.Data.Base;
using KoiPondConstruct.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Data.Repository.Impl
{
    public class CustomerRequestDetailRepository : BaseRepository<TblCustomerRequestDetail>
    {
        public CustomerRequestDetailRepository()
        {
        }

        public CustomerRequestDetailRepository(FA24_SE1702_PRN221_G2_KoiPondConstructContext context) : base(context)
        {
            _context = context;
        }
    }
}
