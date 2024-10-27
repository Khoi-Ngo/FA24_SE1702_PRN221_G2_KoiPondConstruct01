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

namespace KoiPondConstruction.RazorWebApp.Pages.QuotationCost
{
    public class EditModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        //public EditModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context;
        //}
        private readonly FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public EditModel(FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
        public TblQuotationCost TblQuotationCost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblquotationcost =  await _context.TblQuotationCosts.FirstOrDefaultAsync(m => m.Id == id);
            if (tblquotationcost == null)
            {
                return NotFound();
            }
            TblQuotationCost = tblquotationcost;
           ViewData["RequestDetailId"] = new SelectList(_context.TblCustomerRequestDetails, "Id", "Address");
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

            _context.Attach(TblQuotationCost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblQuotationCostExists(TblQuotationCost.Id))
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

        private bool TblQuotationCostExists(long id)
        {
            return _context.TblQuotationCosts.Any(e => e.Id == id);
        }
    }
}
