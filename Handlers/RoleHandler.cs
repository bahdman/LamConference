
using LamConference.ViewModel;

namespace LamConference.Handlers{
    public class RoleHandler{
        // public RoleHandler(RoleManager<IdentityRole> roleManager,
        //     UserManager<IdentityUser> userManager
        // )
        // {
        //     _RoleManager = roleManager;
        //     _UserManager = userManager;
        // }

        // public RoleHandler()
        // {
    
        // }

        

        public async Task<string> RoleCheck(RoleViewModel model)
        {
            // Big TODO:: Simplify this peice of code.
            // Use the enum class.
            var role = "";
            if(model.Role == Role.Admin)
            {
                return "admin";
            }
            else if(model.Role == Role.Finance)
            {
                return "finance";
            }
            else if(model.Role == Role.IT)
            {
                return "it";
            }            
            
            return "Nill";
        }
    }
}