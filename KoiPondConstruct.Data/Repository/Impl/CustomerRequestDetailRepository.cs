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
    public class CustomerRequestDetailRepository : BaseRepository<TblCustomerRequestDetail>
    {
        public CustomerRequestDetailRepository()
        {
        }

        public CustomerRequestDetailRepository(FA24_SE1702_PRN221_G2_KoiPondConstructContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TblCustomerRequestDetail>> GetAllByCuReqIdAsync(long? id)
            => await _context.TblCustomerRequestDetails.Where(d => d.RequestId == id).ToListAsync();

        public  async Task<List<TblCustomerRequestDetail>> GetAllWithMainReuqestAsync()
        => await _context.TblCustomerRequestDetails.Include(d => d.Request).ToListAsync();


        public async Task<TblCustomerRequestDetail> GetByIdWithDesignAsync(long id)
        {
            return await _context.TblCustomerRequestDetails.Include(k => k.TblDesign).FirstOrDefaultAsync(k => k.Id == id);

        }
    }
}
