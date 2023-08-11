using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LamConference.ViewModel{
    public class LoginViewModel{
        public string? Username{get; set;}
        [DataType(DataType.Password)]
        public string? Password{get; set;}
        [DisplayName("Remember Name")]
        public bool RememberMe{get; set;}
        // public Role Role{get; set;}
        //Big Todo :: Add Login by role

    }
}