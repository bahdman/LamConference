using System;
using LamConference.ViewModel;
using LamConference.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class AccountController : Controller{
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser>signInManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _RoleManager = roleManager;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            //Add method that would check for the Role
            if(ModelState.IsValid)
            {
                var user = new IdentityUser{
                    UserName = viewModel.Username,
                };

                var userInstance = await _UserManager.CreateAsync(user, viewModel.Password);
                if(userInstance.Succeeded)
                {
                    RoleViewModel model = new()
                    {
                        Role = viewModel.Role
                    };

                    var rolehandler = new RoleHandler();
                    var roles = await rolehandler.RoleCheck(model);
                    
                    return RedirectToAction(nameof(Login));
                    
                }
            }
            return View(viewModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var instance = _SignInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RemeberMe, false);
                return RedirectToAction("Index", "Home");     
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("", "Home");
        }
    }
}