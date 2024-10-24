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
    public class DesginRepository : BaseRepository<TblDesign>
    {
        public DesginRepository() { }
        public DesginRepository(FA24_SE1702_PRN221_G2_KoiPondConstructContext context) => _context = context;
        public async Task<List<TblDesign>> GetAlLDesignsAsync()
        {
            return await _context.TblDesigns.Include(p => p.RequestDetail).ToListAsync();
        }

    }
}
