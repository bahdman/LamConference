using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LamConference.ViewModel{
    public class LoginViewModel{
        public string? Username{get; set;}
        public string? Password{get; set;}
        [DisplayName("Remember Name")]
        public bool RemeberMe{get; set;}
        public Role Role{get; set;}

    }
}