using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> userManager;
        public readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };
            var identityresult = await userManager.CreateAsync(identityUser,registerViewModel.Password);
            if (identityresult.Succeeded)
            {
                // assign the user role
                var roleResult = await userManager.AddToRoleAsync(identityUser, "User");
                if (roleResult.Succeeded)
                {
                    // show success notification
                    return RedirectToAction("Index", "Home");
                }
            }
           
                // show error message
                return View();
            

        }
        [HttpGet]
        public IActionResult Login(String ReturnUrl)
        {
            var model =new  LoginViewModel{

                ReturnUrl = ReturnUrl
               
            };
            return View(model);
        }
        [HttpPost]
        public  async Task<IActionResult> Login(LoginViewModel loginViewModel) {
            var signin = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            if(  signin !=null && signin.Succeeded ) {
                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                // show successful notification 
                return RedirectToAction("Index", "Home");
            }
            // show erroe message
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

          
    }
}
