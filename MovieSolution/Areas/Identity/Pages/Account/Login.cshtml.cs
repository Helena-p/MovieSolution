using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MovieSolution.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                ReturnUrl = Url.Content("~/");
                if (ModelState.IsValid)
                {
                    // log in per session only
                    var result = await _signInManager.PasswordSignInAsync(
                        Input.Email,
                        Input.Password,
                        false, lockoutOnFailure: false
                        );
                    if (result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User account does not exist.");
                }              
            }

            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
            return Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
