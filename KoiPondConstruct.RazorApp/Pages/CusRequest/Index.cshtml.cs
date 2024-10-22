using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruct.Service;
using Microsoft.AspNetCore.Http;

namespace KoiPondConstruct.RazorApp.Pages.CusRequest
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

        public IList<CustomerRequestListDTO> CustomerRequestListDTOs { get; set; }
        public List<string> AllUniqueStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public SortOptions SortOption { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterStartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterEndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterLastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterEmail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterStatus { get; set; }  // Changed type to string

        public async Task<IActionResult> OnGetAsync()
        {
            // Authentication logic
            var isAuth = _httpContextAccessor.HttpContext.Session.GetString("isAuth");
            if (isAuth != "true")
            {
                return RedirectToPage("/Auth/LoginPage"); // Redirect to login page if not authenticated
            }

            var role = _httpContextAccessor.HttpContext.Session.GetString("role");
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("userid");
            var allRequests = (await _customerRequestService.GetCustomerRequestsAsync()).Data as IList<CustomerRequestListDTO>;

            if (role != "STAFF" && userId.HasValue)
            {
                // Filter by user ID if the role is not STAFF
                allRequests = allRequests.Where(r => r.UserId == userId.Value).ToList();
            }

            // Get all unique statuses from the database
            AllUniqueStatus = await _customerRequestService.GetAllCustomerRequestStatusUniqueAsync();

            // Apply date filters
            if (FilterStartDate.HasValue)
            {
                allRequests = allRequests.Where(r => r.CreatedTime >= FilterStartDate.Value).ToList();
            }
            if (FilterEndDate.HasValue)
            {
                allRequests = allRequests.Where(r => r.CreatedTime <= FilterEndDate.Value).ToList();
            }

            // Apply last name filter
            if (!string.IsNullOrWhiteSpace(FilterLastName))
            {
                allRequests = allRequests.Where(r => r.LastName.Contains(FilterLastName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply email filter
            if (!string.IsNullOrWhiteSpace(FilterEmail))
            {
                allRequests = allRequests.Where(r => r.Email.Contains(FilterEmail, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply status filter
            if (!string.IsNullOrWhiteSpace(FilterStatus))
            {
                allRequests = allRequests.Where(r => r.Status.Equals(FilterStatus, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply sorting
            switch (SortOption)
            {
                case SortOptions.CreatedTime:
                    allRequests = allRequests.OrderBy(r => r.CreatedTime).ToList();
                    break;
                case SortOptions.UserId:
                    allRequests = allRequests.OrderBy(r => r.UserId).ToList();
                    break;
                case SortOptions.FirstName:
                    allRequests = allRequests.OrderBy(r => r.FirstName).ThenBy(r => r.LastName).ToList();
                    break;
                case SortOptions.LastName:
                    allRequests = allRequests.OrderBy(r => r.LastName).ThenBy(r => r.FirstName).ToList();
                    break;
                case SortOptions.Email:
                    allRequests = allRequests.OrderBy(r => r.Email).ToList();
                    break;
                default:
                    break;
            }

            CustomerRequestListDTOs = allRequests;

            // Return the page
            return Page();
        }
    }

    public enum SortOptions
    {
        CreatedTime,
        UserId,
        FirstName,
        LastName,
        Email
    }
}
