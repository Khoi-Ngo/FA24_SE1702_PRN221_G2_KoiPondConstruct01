using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;

namespace KoiPondConstruction.RazorWebApp.Pages.PriceContractCost
{
    public class EditModel : PageModel
    {
        private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public EditModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblPriceContractCost TblPriceContractCost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpricecontractcost =  await _context.TblPriceContractCosts.FirstOrDefaultAsync(m => m.Id == id);
            if (tblpricecontractcost == null)
            {
                return NotFound();
            }
            TblPriceContractCost = tblpricecontractcost;
           ViewData["PriceReferId"] = new SelectList(_context.TblPriceRefers, "Id", "CreatedBy");
           ViewData["QuotationCostId"] = new SelectList(_context.TblQuotationCosts, "Id", "ApprovedBy");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TblPriceContractCost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblPriceContractCostExists(TblPriceContractCost.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TblPriceContractCostExists(long id)
        {
            return _context.TblPriceContractCosts.Any(e => e.Id == id);
        }
    }
}
