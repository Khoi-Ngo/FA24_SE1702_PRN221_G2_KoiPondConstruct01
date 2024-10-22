using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruct.RazorApp.Pages.CusRequest;
using KoiPondConstruct.Service.DTOs;

namespace KoiPondConstruct.RazorApp.Pages.CusReqDetails
{
    public class IndexModel : PageModel
    {
        private readonly CustomerRequestService _customerRequestService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(CustomerRequestService customerRequestService, IHttpContextAccessor httpContextAccessor)
        {
            _customerRequestService = customerRequestService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<GetCustRequestDetailListDTOResponse> GetCustRequestDetailListDTOResponses { get; set; } = default!;

        // Properties for filtering and sorting
        public string Search { get; set; }
        public string SortOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(string search, string sortOrder)
        {
            #region authen
            var isAuth = _httpContextAccessor.HttpContext.Session.GetString("isAuth");
            if (isAuth != "true")
            {
                RedirectToPage("/Auth/LoginPage"); // Redirect to login page if not authenticated
            }
            #endregion

            var role = _httpContextAccessor.HttpContext.Session.GetString("role");
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("userid");
            var allRequestDetails = (await _customerRequestService.GetAllCustomerRequestDetailsAsync()).Data as IList<GetCustRequestDetailListDTOResponse>;

            if (role != "STAFF" && userId.HasValue)
            {
                // Filter by user ID if the role is not STAFF
                allRequestDetails = allRequestDetails.Where(r => r.UserId == userId.Value).ToList();
            }

            // Apply search filtering || this search is used for whole 3 fields
            if (!string.IsNullOrEmpty(search))
            {
                allRequestDetails = allRequestDetails.Where(r =>
                    r.HomeownerFirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.HomeownerLastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.HomeownerPhone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "firstName":
                    allRequestDetails = allRequestDetails.OrderBy(r => r.HomeownerFirstName).ToList();
                    break;
                case "lastName":
                    allRequestDetails = allRequestDetails.OrderBy(r => r.HomeownerLastName).ToList();
                    break;
                case "dateOfBirth":
                    allRequestDetails = allRequestDetails.OrderBy(r => r.HomeownerDateOfBirth).ToList();
                    break;
                case "budget":
                    allRequestDetails = allRequestDetails.OrderBy(r => r.Budget).ToList();
                    break;
                default:
                    // Default sorting can be defined here (e.g., by Id or date created)
                    break;
            }

            GetCustRequestDetailListDTOResponses = allRequestDetails;

            // Return the page
            return Page();
        }
    }
}
