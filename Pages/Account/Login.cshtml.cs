using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using cookie_based_authentication.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace cookie_based_authentication.Pages.Account;

public class Login : PageModel
{
    [BindProperty]
    public Credential Credential { get; set; } = new Credential();
    public void OnGet()
    {

    }
    public async Task<IActionResult> OnPostAsync()
    {

        if (!ModelState.IsValid) return Page();

        // Verify the credential
        if (Credential.UserName == "demo" && Credential.Password == "123456")
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name , "demo"),
                new Claim(ClaimTypes.Email,"demo@mywebsite.com")
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

           return RedirectToPage("/Index");
        }

        return Page();

    }
}