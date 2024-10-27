
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiPondConstruction.Service;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;

namespace KoiPondConstruction.RazorWebApp.Pages.Desgin
{
    public class CreateModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;
        public readonly KoiPondConstructionService _koiPondConstructionService;
        //public CreateModel(KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context;
        //}
        public CreateModel(KoiPondConstructionService koiPondConstructionService)
        {
            _koiPondConstructionService ??= koiPondConstructionService;
        }
        public async Task<IActionResult> OnGet()
        {

            var desgin = await _koiPondConstructionService.GetDesginList();
            if (desgin != null)
            {
                var requestDetail = _koiPondConstructionService.GetRequestDetailList().Result.Data as List<TblCustomerRequestDetail>;
                //ViewData["RequestDetailId"] = new SelectList(requestDetail, "Id", "HomeownerFirstName");
                ViewData["RequestDetailId"] = new SelectList(
            requestDetail.Select(r => new
            {
                Id = r.Id,
                FullName = r.HomeownerFirstName + " " + r.HomeownerLastName
            }),
                "Id",
                "FullName"
                 );
            } 

        
                return Page();
        }


        [BindProperty]
        public TblDesign TblDesign { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.TblDesigns.Add(TblDesign);
            //await _context.SaveChangesAsync();
            await _koiPondConstructionService.Save(TblDesign);

            return RedirectToPage("./Index");
        }
    }
}
