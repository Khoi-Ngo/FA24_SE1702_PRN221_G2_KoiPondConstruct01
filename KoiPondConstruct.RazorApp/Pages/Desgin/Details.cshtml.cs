using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiPondConstruct.Service;
using KoiPondConstruct.Data.Entities;

namespace KoiPondConstruction.RazorWebApp.Pages.Desgin
{
    public class DetailsModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        private readonly KoiPondConstructionService _koiPondConstructionService;

        public DetailsModel(KoiPondConstructionService koiPondConstructionService)
        {
            _koiPondConstructionService = koiPondConstructionService;
        }

        //public DetailsModel(KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context;
        //}

        public TblDesign TblDesign { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tbldesgin = _koiPondConstructionService.GetById(id);
           TblDesign = tbldesgin.Result.Data as TblDesign;
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
            return Page();
        }
    }
}
