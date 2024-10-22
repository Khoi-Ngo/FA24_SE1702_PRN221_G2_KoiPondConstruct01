using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruct.Common;

namespace KoiPondConstruct.RazorApp.Pages.CusRequest
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

        public CustomerRequestDetailDTO customerRequestDetailDTO { get; set; } = new CustomerRequestDetailDTO();

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

            var serviceRes = await _customerRequestService.GetRequestDetailByIdAsync((int)id);
            if (serviceRes.Status == Const.SUCCESS_READ_CODE)
            {
                customerRequestDetailDTO = (CustomerRequestDetailDTO)serviceRes.Data;
            }
            return Page();
        }
    }
}
