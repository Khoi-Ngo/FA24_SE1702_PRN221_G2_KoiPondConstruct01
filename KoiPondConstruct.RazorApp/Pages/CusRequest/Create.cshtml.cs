using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using KoiPondConstruct.Service;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruct.Common;

namespace KoiPondConstruct.RazorApp.Pages.CusRequest
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

        [BindProperty]
        public CustomerRequestDetailDTO CustomerRequestDetailDTO { get; set; }

        [BindProperty]
        public List<CustomerRequestDetailListDTO> CustomerRequestDetailListDTOs { get; set; } = new List<CustomerRequestDetailListDTO>();

        public IActionResult OnGet()
        {
            #region Auth
            var isAuth = _httpContextAccessor.HttpContext.Session.GetString("isAuth");
            if (isAuth != "true")
            {
                return RedirectToPage("/Auth/LoginPage");
            }
            var role = _httpContextAccessor.HttpContext.Session.GetString("role");

            if (!role.Equals("CUSTOMER"))
            {
                return RedirectToPage("/Index");
            }
            #endregion

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            #region Auth
            var isAuth = _httpContextAccessor.HttpContext.Session.GetString("isAuth");
            if (isAuth != "true")
            {
                return RedirectToPage("/Auth/LoginPage");
            }
            var role = _httpContextAccessor.HttpContext.Session.GetString("role");

            if (!role.Equals("CUSTOMER")){
                return RedirectToPage("/Index");
            }
            #endregion

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Assign the detail list to the DTO
            CustomerRequestDetailDTO.customerRequestDetailListDTOs = CustomerRequestDetailListDTOs;
            // Call the service layer to create the customer request with details
            var result = await _customerRequestService.CreateCustomerRequestAsync(CustomerRequestDetailDTO, (int)_httpContextAccessor.HttpContext.Session.GetInt32("userid"));

            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                // Handle failure case
                ModelState.AddModelError(string.Empty, "An error occurred while creating the request.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
