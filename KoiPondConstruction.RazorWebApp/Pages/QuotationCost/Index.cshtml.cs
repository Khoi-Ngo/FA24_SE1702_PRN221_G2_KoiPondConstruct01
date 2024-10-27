using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;
using KoipondContruction.Service;

namespace KoiPondConstruction.RazorWebApp.Pages.QuotationCost
{
    public class IndexModel : PageModel
    {
        //private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;
        private readonly TblQuotationCostSevice _tblQuotationCostSevice;

        //public IndexModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}
        public IndexModel(TblQuotationCostSevice tblQuotationCostSevice)
        {
            _tblQuotationCostSevice = tblQuotationCostSevice ?? throw new ArgumentNullException(nameof(tblQuotationCostSevice));
        }

        public IList<TblQuotationCost> TblQuotationCost { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                var result = await _tblQuotationCostSevice.GetAll();
                TblQuotationCost = result.Data as IList<TblQuotationCost>;
            }
            else
            {
                var result = await _tblQuotationCostSevice.Search(SearchTerm);
                TblQuotationCost = result.Data as IList<TblQuotationCost>;
            }
        }
    }
}
