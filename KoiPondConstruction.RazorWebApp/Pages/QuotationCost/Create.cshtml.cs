using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;


namespace KoiPondConstruction.RazorWebApp.Pages.QuotationCost
{
    public class CreateModel : PageModel
    {
        private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public CreateModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context;
        }
        //private readonly FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        //public CreateModel(FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}

        public IActionResult OnGet()
        {
        ViewData["RequestDetailId"] = new SelectList(_context.TblCustomerRequestDetails, "Id", "Address");
            return Page();
        }

        [BindProperty]
        public TblQuotationCost TblQuotationCost { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblQuotationCosts.Add(TblQuotationCost);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
