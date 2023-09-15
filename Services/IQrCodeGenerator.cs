using LamConference.HandlerModels;

namespace LamConference.Services{
    public interface IQrCodeGenerator{
        Stream GenerateCode(MailHandlerModel model);
    }
}