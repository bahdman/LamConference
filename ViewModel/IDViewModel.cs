using System.ComponentModel.DataAnnotations;

namespace LamConference.ViewModel{
    public class IDViewModel{
        [Required(ErrorMessage = "Field cannot be blank")]
        public Guid RefID{get; set;}
    }
}