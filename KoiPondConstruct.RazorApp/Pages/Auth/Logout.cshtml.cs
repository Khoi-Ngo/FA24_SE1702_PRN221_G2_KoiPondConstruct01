using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiPondConstruct.RazorApp.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnPost()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToPage("/Auth/LoginPage"); // Redirect to login page after logout
        }
    }
}
