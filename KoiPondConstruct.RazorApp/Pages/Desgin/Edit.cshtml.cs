using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Service;
using KoiPondConstruct.Service;
using KoiPondConstruct.Data.Entities;

namespace KoiPondConstruction.RazorWebApp.Pages.Desgin
{
    public class EditModel : PageModel
    {

        private readonly KoiPondConstructionService _koiPondConstructionService;

        public EditModel(KoiPondConstructionService koiPondConstructionService)
        {
            _koiPondConstructionService = koiPondConstructionService;   
        }
        //private readonly KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        //public EditModel(KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context;
        //}

        [BindProperty]
        public TblDesign TblDesign { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _design = _koiPondConstructionService.GetById(id);
            TblDesign = _design.Result.Data as TblDesign;
            var requestDetail = _koiPondConstructionService.GetRequestDetailList().Result.Data as List<TblCustomerRequestDetail>;
            ViewData["RequestDetailId"] = new SelectList(
         requestDetail.Select(r => new
            {
         Id = r.Id,
         FullName = r.HomeownerFirstName + " " + r.HomeownerLastName
            }),
             "Id",
             "FullName"
          );
            //if (tbldesign == null)
            //{
            //    return NotFound();
            //}
            //TblDesign = tbldesign;
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
            await _koiPondConstructionService.Save(TblDesign);
            //_context.Attach(TblDesign).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TblDesignExists(TblDesign.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        //private bool TblDesignExists(long id)
        //{
        //    return _context.TblDesigns.Any(e => e.Id == id);
        //}
    }
}
