using System.ComponentModel;

namespace LamConference.ViewModel{
    public class RegisterViewModel{
        public string? Username{get; set;}
        public string? Password{get; set;}
        [DisplayName("Confirm Password")]
        public string? ConfirmPassword{get; set;}
        public Role Role{get; set;}

    }

    public enum Role{
        Finance,
        Admin,
        IT
    }
}