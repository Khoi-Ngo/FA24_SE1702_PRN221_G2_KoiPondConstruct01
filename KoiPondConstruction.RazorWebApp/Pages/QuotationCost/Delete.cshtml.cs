﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;

namespace KoiPondConstruction.RazorWebApp.Pages.QuotationCost
{
    public class DeleteModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        //public DeleteModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context;
        //}
        private readonly FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public DeleteModel(FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
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

            var tblquotationcost = await _context.TblQuotationCosts.FirstOrDefaultAsync(m => m.Id == id);

            if (tblquotationcost == null)
            {
                return NotFound();
            }
            else
            {
                TblQuotationCost = tblquotationcost;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblquotationcost = await _context.TblQuotationCosts.FindAsync(id);
            if (tblquotationcost != null)
            {
                TblQuotationCost = tblquotationcost;
                _context.TblQuotationCosts.Remove(TblQuotationCost);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}