using System;
using LamConference.ViewModel;
using LamConference.Handlers;
using LamConference.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class AccountController : Controller{
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccount _service;

        public AccountController(UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser>signInManager, IAccount service)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
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
            if(ModelState.IsValid)
            {
                RoleViewModel model = new()
                {
                    Role = viewModel.Role
                };

                var rolehandler = new RoleHandler();
                var role = rolehandler.RoleCheck(model);

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

        private async Task<string> GetSignedInUserRole()
        { 
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userRole = await _userManager.GetRolesAsync(user);

            return userRole[0];                  
        }

        public async Task<ActionResult> Login()
        {
            var status = _SignInManager.IsSignedIn(User);
            if(status)
            {
                var userRole = await GetSignedInUserRole();
                
                return RedirectToAction("Dashboard", userRole);
            }

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
                    var returnUrl = await ReturnUrl(viewModel);
                    if(returnUrl != null || returnUrl != "")
                    {
                        return RedirectToAction("Dashboard", returnUrl);
                    }

                    return RedirectToAction("ViewGeneratedIDs", "RefID");
                }   
                this.ModelState.AddModelError("Username", "Username or Password is invalid");
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public async Task<string?> ReturnUrl(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var userRole = await _userManager.GetRolesAsync(user);

                return userRole[0];                
            }

            return null;            
        }
    }
}