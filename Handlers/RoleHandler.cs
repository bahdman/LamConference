using Microsoft.AspNetCore.Identity;
// using LamConference.Models;
using LamConference.ViewModel;

namespace LamConference.Handlers{
    public class RoleHandler{
        private readonly RoleManager<IdentityRole> _RoleManager;

        public RoleHandler()
        {
    
        }

        public RoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _RoleManager = roleManager;
        }

        public async Task<bool> RoleCheck(RoleViewModel model)
        {
            var role = "";
            if(model.Role == Role.Admin)
            {
                role = "admin";
            }
            else if(model.Role == Role.Finance)
            {
                role = "finance";
            }
            else if(model.Role == Role.IT)
            {
                role ="it";
            }
            else{
                // return View(viewModel);
            }
            IdentityRole IDRole = new ()
            {
                Name = role
            };
            var test = await _RoleManager.CreateAsync(IDRole);
            return test.Succeeded;
        }
    }
}