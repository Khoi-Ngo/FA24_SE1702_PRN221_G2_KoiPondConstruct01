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
    public class IndexModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;
        private KoiPondConstructionService _koiPondConstructionService;

        //public IndexModel(KoiPondConstruction.Data.Models.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}
        public IndexModel(KoiPondConstructionService koiPondConstructionService)
        {
            _koiPondConstructionService = koiPondConstructionService;
        }
        public IList<TblDesign> TblDesign { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchValue { get; set; }


        public async Task OnGetAsync()
        {
            //TblDesign = await _context.TblDesigns
            //    .Include(t => t.RequestDetail).ToListAsync();

            var result = await _koiPondConstructionService.GetAll();
            if (result.Status > 0)
            {
                if (string.IsNullOrEmpty(SearchValue))
                {
                 TblDesign = (await _koiPondConstructionService.GetDesginList()).Data as IList<TblDesign>;
                        
                }
                else
                {
                    var searchResult = await _koiPondConstructionService.SearchDesignByCreate(SearchValue.ToLower());
                    if (searchResult != null && searchResult.Data != null)
                    {
                        TblDesign = searchResult.Data as IList<TblDesign>;
                    }
                    else
                    {
                        TblDesign = new List<TblDesign>();
                    }
                }



        }
    }
    }
}
