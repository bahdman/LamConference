using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LamConference.Models;

namespace LamConference.ViewModel{
    public class RegistrationViewModel{
        [DisplayName("First Name")]
        public string? FirstName{get; set;}
        [DisplayName("Last Name")]
        public string? LastName{get; set;}
        [DisplayName("Phone Number")]
        public string? Telephone{get; set;}
        [DataType(DataType.EmailAddress)]
        public string? Email{get; set;}
        public Level Level{get; set;}
        public Department Department{get; set;}
        public Guid RefID{get; set;}
    }
}