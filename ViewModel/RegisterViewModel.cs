using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LamConference.ViewModel{
    public class RegisterViewModel{
        public string? Username{get; set;}
        [DataType(DataType.Password)]
        public string? Password{get; set;}
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword{get; set;}
        public Role Role{get; set;}

    }

    public enum Role{
        Finance,
        Admin,
        IT
    }
}