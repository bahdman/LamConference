using System;
using LamConference.ViewModel;
using LamConference.Handlers;
using LamConference.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class AccountController : Controller{
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IAccount _service;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser>signInManager,
            RoleManager<IdentityRole> roleManager, IAccount service
        )
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _RoleManager = roleManager;
            _service = service;
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
                RoleViewModel model = new()
                {
                    Role = viewModel.Role
                };

                var rolehandler = new RoleHandler();
                var role = await rolehandler.RoleCheck(model);
                if(role == "Nill")
                {
                    return View(viewModel);
                }
                bool instance = await _service.Register(viewModel, role);

                if(instance == true)
                {
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
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                bool instance = await _service.Login(viewModel);
                if(instance == true)
                {
                    return RedirectToAction("Index", "Home");  
                }     
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("", "Home");
        }
    }
}