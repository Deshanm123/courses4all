using Courses4All.Areas.Identity.Pages.Account;
using Courses4All.Data;
using Courses4All.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Courses4All.Controllers
{
    public class UserAuthController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public UserAuthController( ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
          _userManager = userManager;
          _signInManager = signInManager;
          _context = context;
        }

        [AllowAnonymous] //pubblically accesible without any authorization
        [HttpPost]
        [ValidateAntiForgeryToken] //Prevent cross-site  request forgeries
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
                //check wheether login attempt is successfull or not
            loginViewModel.LoginInvalid ="true";
            if(ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
                if (loginResult.Succeeded)
                {
                   loginViewModel.LoginInvalid = "";
                }
                else
                {
                   ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return PartialView("_UserLoginPartial",loginViewModel);

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout (string returnUrl= null)
        {
            await _signInManager.SignOutAsync();
            if(returnUrl != null)
            {
                return LocalRedirect(returnUrl);//LocalRedirect will throw an exception if a non-local URL is specified.
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }




    }
}
