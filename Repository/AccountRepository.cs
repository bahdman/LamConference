using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Repository{
    public class AccountRepository : IAccount{

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        
        public async Task<bool>  Register(RegisterViewModel viewModel, string role)
        {
            IdentityUser user = new()
            {
                UserName = viewModel.Username
            };
            //Big TODO::Add exception handler.
            var instance = await _userManager.CreateAsync(user, viewModel.Password);
            if(instance.Succeeded)
            {
                IdentityRole IDRole = new()
                {
                    Name = role
                };

                var roleInstance = await _roleManager.CreateAsync(IDRole);
                var userRoleInstance = await _userManager.AddToRoleAsync(user, role);
                if(roleInstance.Succeeded && userRoleInstance.Succeeded)
                {
                    return true;
                }  
            }
            return false;
        }

        [ValidateAntiForgeryToken]
        public async Task<bool> Login(LoginViewModel viewModel)
        {
            var instance = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, false);
            if(instance.Succeeded)
            {
                return true;
            }
            return false;
        }

        
    }
}