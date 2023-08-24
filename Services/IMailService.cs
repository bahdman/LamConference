using LamConference.HandlerModels;

namespace LamConference.Services{
    public interface IMailService{
        Task<bool> SendMail(MailHandlerModel model);
    }
}