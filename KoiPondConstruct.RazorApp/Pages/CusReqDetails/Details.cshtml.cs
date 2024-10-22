using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruct.Common;
using KoiPondConstruct.Service.DTOs;

namespace KoiPondConstruct.RazorApp.Pages.CusReqDetails
{
    public class DetailsModel : PageModel
    {
        private readonly CustomerRequestService _customerRequestService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DetailsModel(CustomerRequestService customerRequestService, IHttpContextAccessor httpContextAccessor)
        {
            _customerRequestService = customerRequestService;
            _httpContextAccessor = httpContextAccessor;
        }
        public TblCustomerRequestDetail TblCustomerRequestDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            #region authen
            var isAuth = _httpContextAccessor.HttpContext.Session.GetString("isAuth");
            if (isAuth != "true")
            {
                return RedirectToPage("/Auth/LoginPage");
            }
            #endregion
            if (id == null)
            {
                return NotFound();
            }

            var serviceRes = await _customerRequestService.GetRequestDetailDetailByIdAsync(id);
            if (serviceRes.Status == Const.SUCCESS_READ_CODE)
            {
                TblCustomerRequestDetail = (TblCustomerRequestDetail)serviceRes.Data;
            }
            return Page();
        }
    }
}
