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

namespace KoiPondConstruct.RazorApp.Pages.CusRequest
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
        public GetCustomerRequestUpdateDTO getCustomerRequestUpdateDTO { get; set; } = new GetCustomerRequestUpdateDTO();

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

            var serviceRes = await _customerRequestService.GetCustomerUpdateResponseByIdAsync(id);
            if (serviceRes.Status == Const.SUCCESS_READ_CODE)
            {
                getCustomerRequestUpdateDTO = (GetCustomerRequestUpdateDTO)serviceRes.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the service layer to update the customer request
            var serviceResponse = await _customerRequestService.UpdateCustomerRequestAsync(getCustomerRequestUpdateDTO);

            if (serviceResponse.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                // Handle errors (e.g., show a message or log the issue)
                ModelState.AddModelError("", "Unable to update the customer request.");
                return Page();
            }
        }


    }
}
