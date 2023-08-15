using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LamConference.Models{
    public class StudentData{
        public Guid Id{get; set;}
        [DisplayName("First Name")]
        public string? FirstName{get; set;}
        [DisplayName("Last Name")]
        public string? LastName{get; set;}
        public double Telephone{get; set;}
        public Level Level{get; set;}
        public string? Email{get; set;}
        public Department Department{get; set;}
        public Guid RefId{get; set;}
    }

    public enum Level{
        [Display(Name= "100")]
        FirstYear = 100,
        [Display(Name = "200")]
        SecondYear = 200,
        [Display(Name = "300")]
        ThirdYear = 300,
        [Display(Name = "400")]
        FourthYear = 400,
        [Display(Name = "500")]
        FifthYear = 500
    }

    public enum Department{
        [Display(Name = "Accounting")]
        Accounting,
        [Display(Name = "Business Administration")]
        BusinessAdministration,
        [Display(Name = "Biochemistry")]
        Biochemistry,
        [Display(Name = "Computer Science")]
        ComputerScience,
        [Display(Name = "Cyber Security")]
        CyberSecurity,
        [Display(Name = "Economics")]
        Economics,
        [Display(Name ="Finance")]
        Finance,
        [Display(Name = "Informaton Technology")]
        InformationTechnology,
        [Display(Name = "International Relations")]
        InternationalRelations,
        [Display(Name = "Marketing")]
        Marketing,
        [Display(Name = "Mass Communication")]
        MassCommunication,
        [Display(Name = "Microbiology")]
        Micorbiology,
        [Display(Name = "Software Engineering")]
        SoftwareEngineering
    }
}