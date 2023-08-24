
using LamConference.ViewModel;

namespace LamConference.Handlers{
    public class RoleHandler{
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