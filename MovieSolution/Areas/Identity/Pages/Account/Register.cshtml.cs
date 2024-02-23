using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MovieSolution.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(SignInManager<IdentityUser> signInManager, 
                             UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //_roleManager = roleManager;
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
            ReturnUrl = Url.Content("~/");
            if(ModelState.IsValid)
            {
                // Create instance of IdentityUser
                var identity = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                // Create user, password entered separately due to hashing and separate validation handling
                var result = await _userManager.CreateAsync(identity, Input.Password);

                // using Claims
                var claim = new Claim("Role", Input.Role.ToLower());
                var claimResult = await _userManager.AddClaimAsync(identity, claim);

                // SignIn user if success/valid
                if(result.Succeeded && claimResult.Succeeded)
                {
                    // isPersistent: false = only persist per session
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect(ReturnUrl);
                }
            }
            else
                {
                    ModelState.AddModelError(string.Empty, "Email or password is incorrect");
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

            [Required]
            public string Role { get; set; }
        }
    }
}
