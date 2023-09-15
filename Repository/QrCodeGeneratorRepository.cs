using System.Drawing;
using System.Drawing.Imaging;
using LamConference.HandlerModels;
using LamConference.Services;
using QRCoder;

namespace LamConference.Repository{
    public class QrCodeGeneratorRepository : IQrCodeGenerator
    {
        
        public Stream GenerateCode(MailHandlerModel model)
        {
            MemoryStream ms = new();
            var embeddedMessage = $@"
            LamConference guest : {model.Name}
            
            Reference ID : 
            {model.Message}



            Guest mail : 
            {model.Email}
            ";

            
            using QRCodeGenerator generator = new ();
            using QRCodeData qrData = generator.CreateQrCode(embeddedMessage, QRCodeGenerator.ECCLevel.Q);
            using QRCode code = new(qrData);

            Bitmap image = code.GetGraphic(20);
            image.Save(ms, ImageFormat.Jpeg);
            
            return ms;            
        }
    }
}