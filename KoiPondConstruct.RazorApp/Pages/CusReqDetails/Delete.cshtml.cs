using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Common;
using KoiPondConstruct.Service;

namespace KoiPondConstruct.RazorApp.Pages.CusReqDetails
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomerRequestService _customerRequestService;


        public DeleteModel(CustomerRequestService customerRequestService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _customerRequestService = customerRequestService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _customerRequestService.DeleteCustomerRequestDetailByIdAsync(id);

            if (result.Status != Const.SUCCESS_DELETE_CODE)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the request.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
