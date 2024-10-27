﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;

        public IndexModel(KoiPondConstruction.Data.DBContext.FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context;
        }

        public IList<TblPriceRefer> TblPriceRefer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TblPriceRefer = await _context.TblPriceRefers.ToListAsync();
        }
    }
}