using System.ComponentModel;
using LamConference.Models;

namespace LamConference.ViewModel{
    public class ITDashboardViewModel{
        public string? FirstName{get; set;}
        public string? LastName{get; set;}
        public string? Telephone{get; set;}
        [DisplayName("Department")]
        public Department Department{get; set;}
        [DisplayName("Level")]
        public Level Level{get; set;}
        public Guid RefID{get; set;}
        [DisplayName("Available IDs")]
        public int AvailableRefID{get; set;}
        [DisplayName("Generated IDs")]
        public int TotalGeneratedID{get; set;}
        [DisplayName("Registered")]
        public int TotalRegisteredStudents{get; set;}
        [DisplayName("Estimated Amount")]
        public int EstimatedAmount{get; set;}
    }
}