using System;
using LamConference.ViewModel;
using LamConference.Handlers;
using LamConference.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class AccountController : Controller{
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly IAccount _service;

        public AccountController(SignInManager<IdentityUser>signInManager, IAccount service)
        {
            _SignInManager = signInManager;
            _service = service;
        }

        public ActionResult Register()
        {
            var test = Guid.NewGuid();
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("", "Home");
        }
    }
}