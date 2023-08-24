using System.ComponentModel.DataAnnotations;

namespace LamConference.ViewModel{
    public class GenerateIDViewModel{
        [Required(ErrorMessage = "Kindly input a value")]
        [Range(1,100, ErrorMessage = "Value should be between {1} and {2}")]
        public int Value{get; set;}
    }
}