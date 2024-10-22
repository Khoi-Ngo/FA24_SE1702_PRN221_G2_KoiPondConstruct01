using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using KoiPondConstruct.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiPondConstruct.RazorApp.Pages.Auth
{
    public class LoginPageModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginPageModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            var checkLogin = await _authService.LoginAsync(Username, Password);

            if (checkLogin)
            {
                return RedirectToPage("/Index"); // Redirect to home page
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page(); // Remain on the same page
            }
        }
    }
}
