using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;

namespace KoiPondConstruction.RazorWebApp.Pages.PriceContractCost
{
    public class CreateModel : PageModel
    {
        private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public CreateModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PriceReferId"] = new SelectList(_context.TblPriceRefers, "Id", "CreatedBy");
        ViewData["QuotationCostId"] = new SelectList(_context.TblQuotationCosts, "Id", "ApprovedBy");
            return Page();
        }

        [BindProperty]
        public TblPriceContractCost TblPriceContractCost { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblPriceContractCosts.Add(TblPriceContractCost);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
