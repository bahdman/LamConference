using LamConference.Models;

namespace LamConference.ViewModel{
    public class CheckRegViewModel{
        public Guid RefID{get; set;}
        public string FirstName{get; set;}
        public string LastName{get; set;}
        public string Email{get; set;}
        public Department Department{get; set;}
        public Level Level{get; set;}
    }
}