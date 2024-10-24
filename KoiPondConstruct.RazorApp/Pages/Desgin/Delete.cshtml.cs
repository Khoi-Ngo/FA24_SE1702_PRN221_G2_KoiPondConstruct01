using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Service;
using KoiPondConstruct.Service;
using KoiPondConstruct.Data.Entities;

namespace KoiPondConstruction.RazorWebApp.Pages.Desgin
{
    public class DeleteModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public readonly KoiPondConstructionService _koiPondConstructionService;

        //public DeleteModel(KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context;
        //}
        public DeleteModel(KoiPondConstructionService koiPondConstruction)
        {
            _koiPondConstructionService = koiPondConstruction;
        }

        [BindProperty]
        public TblDesign TblDesign { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbldesign = _koiPondConstructionService.GetById((id));
            TblDesign = tbldesign.Result.Data as TblDesign;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (TblDesign == null)
            {
                return NotFound();
            }
            var tbldesign = _koiPondConstructionService.DeleteAsync(TblDesign);
            //if (tbldesign != null)
            //{
            //    TblDesign = tbldesign;
            //    _context.TblDesigns.Remove(TblDesign);
            //    await _context.SaveChangesAsync();    
            //}

            return RedirectToPage("./Index");
        }
    }
}
