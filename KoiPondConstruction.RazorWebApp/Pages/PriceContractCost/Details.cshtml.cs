using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;

namespace KoiPondConstruction.RazorWebApp.Pages.PriceContractCost
{
    public class DetailsModel : PageModel
    {
        private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public DetailsModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context;
        }

        public TblPriceContractCost TblPriceContractCost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpricecontractcost = await _context.TblPriceContractCosts.FirstOrDefaultAsync(m => m.Id == id);
            if (tblpricecontractcost == null)
            {
                return NotFound();
            }
            else
            {
                TblPriceContractCost = tblpricecontractcost;
            }
            return Page();
        }
    }
}
