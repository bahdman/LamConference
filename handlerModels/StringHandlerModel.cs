namespace LamConference.HandlerModels{
    public class StringHandlerModel{
        public string? FirstName{get; set;}
        public string? LastName{get; set;}
        //Todo:: Edit this properties => have just one property that would take in all the strings
        //Todo:: achievable by having a list instance of this model.
        //Todo:: List instance would be validated against string check method in ModelHanlder class
    }
}