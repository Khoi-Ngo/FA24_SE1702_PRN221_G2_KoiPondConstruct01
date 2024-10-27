using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;

namespace KoiPondConstruction.RazorWebApp.Pages.PriceRefer
{
    public class DeleteModel : PageModel
    {
        private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public DeleteModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblPriceRefer TblPriceRefer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpricerefer = await _context.TblPriceRefers.FirstOrDefaultAsync(m => m.Id == id);

            if (tblpricerefer == null)
            {
                return NotFound();
            }
            else
            {
                TblPriceRefer = tblpricerefer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpricerefer = await _context.TblPriceRefers.FindAsync(id);
            if (tblpricerefer != null)
            {
                TblPriceRefer = tblpricerefer;
                _context.TblPriceRefers.Remove(TblPriceRefer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
