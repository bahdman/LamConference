using LamConference.HandlerModels;

namespace LamConference.Services{
    public interface IMailService{
        bool SendMail(MailHandlerModel model);
    }
}