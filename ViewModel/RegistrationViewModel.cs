using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using LamConference.Models;

namespace LamConference.ViewModel{
    public class RegistrationViewModel{
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Cannot be blank")]
        [RegularExpression(@"^[ ]?[a-zA-Z]+[ ]*$", ErrorMessage ="Name is not valid")]
        public string? FirstName{get; set;}
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Cannot be blank")]
        [RegularExpression(@"^[ ]?[a-zA-Z]+[ ]*$", ErrorMessage ="Name is not valid")]
        public string? LastName{get; set;}
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Cannot be blank")]
        [RegularExpression(@"^[+]?[0789]?[0-9]{10}$", ErrorMessage = "Not a valid phonenumber")]
        public string? Telephone{get; set;}
        [Required(ErrorMessage = "Cannot be blank")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string? Email{get; set;}
        [EnumDataType(typeof(Level), ErrorMessage = "Select a valid field")]
        public Level Level{get; set;}
        [EnumDataType(typeof(Department), ErrorMessage = "Select a valid field")]
        public Department Department{get; set;}
        public Guid RefID{get; set;}
    }
}