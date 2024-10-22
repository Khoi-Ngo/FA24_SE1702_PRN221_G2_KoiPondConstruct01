using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Common;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruct.Service;
using System.Diagnostics;

namespace KoiPondConstruct.RazorApp.Pages.CusReqDetails
{
    public class EditModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomerRequestService _customerRequestService;

        public EditModel(CustomerRequestService customerRequestService, IHttpContextAccessor httpContextAccessor)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //donot alow to update the main customer request id
            var result = await _customerRequestService.UpdateCustomerDetailAsync(TblCustomerRequestDetail);
            if (result.Status != Const.SUCCESS_UPDATE_CODE)
            {
                // Handle failure case
                ModelState.AddModelError(string.Empty, "An error occurred while updating the request.");
                return Page();
            }


            return RedirectToPage("./Index");
        }

    }
}
