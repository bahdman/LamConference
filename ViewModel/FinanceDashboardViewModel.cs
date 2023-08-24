using System.ComponentModel;
using LamConference.Models;

namespace LamConference.ViewModel{
    public class FinanceDashboardViewModel{
        [DisplayName("First Name")]
        public string? FirstName{get; set;}
        [DisplayName("Last Name")]
        public string? LastName{get; set;}
        public string? Email{get; set;}
        [DisplayName("Department")]
        public Department Department{get; set;}
        [DisplayName("Level")]
        public Level Level{get; set;}
        public Guid RefID{get; set;}
        [DisplayName("Available IDs")]
        public int AvailableRefID{get; set;}
        [DisplayName("Generated IDs")]
        public int TotalGeneratedID{get; set;}
        [DisplayName("Registered Students")]
        public int TotalRegisteredStudents{get; set;}
        //Big TODO:: add unused IDs
    }
}