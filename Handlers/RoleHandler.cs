
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

        

        public string RoleCheck(RoleViewModel model)
        {
            bool enumCheck = Enum.IsDefined(model.Role);
            if(enumCheck)
            {
               return Enum.GetName<Role>(model.Role);
            }           
            return "Nill";
        }
    }
}