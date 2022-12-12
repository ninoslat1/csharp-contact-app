using LoginApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Login Model { get; set; }


        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityRes = await signInManager.PasswordSignInAsync(Model.Name, Model.Password, Model.RememberMe, false);
                if (identityRes.Succeeded)
                {
                    if(returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                ModelState.AddModelError("", "Name or password incorrect");
            }
            return Page(); 
        }
    }
}
