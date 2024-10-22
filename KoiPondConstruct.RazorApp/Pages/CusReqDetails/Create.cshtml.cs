using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruct.Common;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruction.Service.Base;

namespace KoiPondConstruct.RazorApp.Pages.CusReqDetails
{
    public class CreateModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomerRequestService _customerRequestService;

        public CreateModel(
            CustomerRequestService customerRequestService,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _customerRequestService = customerRequestService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Check authentication
            var isAuth = _httpContextAccessor.HttpContext.Session.GetString("isAuth");
            if (isAuth != "true")
            {
                return RedirectToPage("/Auth/LoginPage");
            }

            // Retrieve customer requests
            ServiceResult temp = await _customerRequestService.GetCustomerRequestsAsync();
            if (temp.Data is not List<CustomerRequestListDTO> dataBox)
            {
                ModelState.AddModelError("", "Failed to retrieve customer requests.");
                return Page();
            }

            // Check user role and ID from the session
            var role = _httpContextAccessor.HttpContext.Session.GetString("role");
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("userid");

            // Filter data based on role and user ID
            if (role != "STAFF" && userId.HasValue)
            {
                dataBox = dataBox.Where(r => r.UserId == userId.Value).ToList();
            }

            // Handle the case where there are no requests available
            if (dataBox == null || !dataBox.Any())
            {
                ModelState.AddModelError("", "No customer requests found for your user.");
                return Page();
            }

            // Populate the dropdown for RequestId
            ViewData["RequestId"] = new SelectList(dataBox, "Id", "Description");

            // Initialize the model to avoid null reference in the view
            TblCustomerRequestDetail = new TblCustomerRequestDetail();

            return Page();
        }

        [BindProperty]
        public TblCustomerRequestDetail TblCustomerRequestDetail { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, return the page with validation errors
                return Page();
            }

            var result = await _customerRequestService.CreateCustomRequestDetailAsync(TblCustomerRequestDetail);

            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the request.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
