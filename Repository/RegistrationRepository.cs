using LamConference.Handlers;
using LamConference.Models;
using LamConference.Services;
using LamConference.ViewModel;
using LamConference.HandlerModels;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LamConference.Repository{
    public class RegistrationRepository : ReferenceIDHandler, IRegistration, IMailService{

        private readonly Data.AppContext _context;
        private readonly IQrCodeGenerator _service;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnv;

        public RegistrationRepository(Data.AppContext context,
            IQrCodeGenerator service,
            IConfiguration config,
            IWebHostEnvironment hostEnv
        ) : base(context)
        {
            _context = context;
            _service = service;
            _config = config;
            _hostEnv = hostEnv;
        }

        public async Task<bool> IdCheck(IDViewModel viewModel)
        {
            if(viewModel != null)
            {
                var check = await FindID(viewModel.RefID);
                if(check != null)
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        public async Task<bool> ValidateEmail(EmailViewModel model)
        {
            var isEmailInDb = await _context.StudentData.AnyAsync(m => m.Email == model.Email);
            if(isEmailInDb)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Registration(RegistrationViewModel viewModel)
        {
            var isRefIDValid = await FindID(viewModel.RefID);
            if(isRefIDValid == null || isRefIDValid.Id == Guid.Empty)
            {
                return false;
            }

            StudentData data = new(){
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Telephone = viewModel.Telephone,
                Level = viewModel.Level,
                Department = viewModel.Department,
                RefId = viewModel.RefID
            };

            MailHandlerModel model = new(){
                Name = data.FirstName,
                Email = data.Email,
                Message = data.RefId.ToString()
            };

            //Big TODO::Add exception handler
            try{
                await _context.StudentData.AddAsync(data);
                _context.ReferenceIDs.Remove(isRefIDValid);
                await _context.SaveChangesAsync();
            }catch(DbException ex)
            {
                Console.Write(ex.Message[0]);
                return false;
            }

            try{
                var instance = await SendMail(model);
                if(instance)
                {
                    return true;
                }

                return false;

            } catch(Exception ex)
            {
                Console.Write(ex.Message[0]);
                return false;
            }             
        }


        public async  Task<bool> SendMail(MailHandlerModel model)
        {
            //Big TODO::Add exception handler.

            try{

                using var client = new SmtpClient();
                client.Host = _config["EmailLogs:Host"];
                client.Port = Convert.ToInt32(_config["EmailLogs:Port"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_config["EmailLogs:MailingAddress"],
                _config["EmailLogs:SecurityKey"]
                );

                MailMessage message = new MailMessage(
                    from: new MailAddress(_config["EmailLogs:MailingAddress"], "LAM Conference"),
                    to: new MailAddress(model.Email, model.Name)
                );
                
                message.IsBodyHtml = true;
                var filePath = _hostEnv.WebRootPath + Path.DirectorySeparatorChar.ToString() + "MailTemplates" 
                                + Path.DirectorySeparatorChar.ToString() + "RegistrationSuccessful.html";

                StreamReader reader = new(filePath);
                var mailBody = await reader.ReadToEndAsync();
                reader.Close();

                mailBody = mailBody.Replace("[Name]", model.Name);

                
                message.Subject = "Hello from Lam Conference";
                message.Body = mailBody;

                using (var qrCode = ((QrCodeGeneratorRepository)_service).GenerateCode(model))
                {
                    qrCode.Seek(0, SeekOrigin.Begin);

                    
                    Attachment attachment = new(qrCode, "qrcode.jpg");
                                    
                    message.Attachments.Add(attachment);
                    
                    client.Send(message);
                    return true;
               }
            }catch(Exception ex)
            {
                Console.Write(ex.Message[0]);
                return false;
            }           
        }
    }
}