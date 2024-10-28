using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruct.Service;
using KoiPondConstruct.Common;

namespace KoiPondConstruct.RazorApp.Pages.CusRequest
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


        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //call service to delete all customer and other relations
            await _customerRequestService.DeleteByIdAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
