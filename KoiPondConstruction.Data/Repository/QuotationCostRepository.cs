//using KoiPondConstruction.Data.Base;
//using KoiPondConstruction.Data.DBContext;
//using KoiPondConstruction.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KoiPondConstruction.Data.Repository
//{
//    public class QuotationCostRepository : GenericRepository<TblQuotationCost>
//    {
//        public QuotationCostRepository() { }
//        public QuotationCostRepository(FA24_SE1702_PRN221_G2_KoiPondConstructionContext context) => _context = context;


//    }
//}
using KoiPondConstruction.Data.Base;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KoiPondConstruction.Data.Repository
{
    public class QuotationCostRepository : GenericRepository<TblQuotationCost>
    {
        // Default constructor can be removed since it's not useful without the context
        public QuotationCostRepository(FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
            : base(context)  // Pass the context to the base class constructor
        {
        }
        public async Task AddAsync(TblQuotationCost entity)
        {
            await _context.Set<TblQuotationCost>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TblQuotationCost entity)
        {
            _context.Set<TblQuotationCost>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TblQuotationCost>> SearchAsync(string searchTerm)
        {
            return await _context.TblQuotationCosts
                .Where(q => q.Id.ToString().Contains(searchTerm) || q.ContentText.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
