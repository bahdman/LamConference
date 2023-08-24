using LamConference.Handlers;
using LamConference.Models;
using LamConference.Services;
using LamConference.ViewModel;
using LamConference.HandlerModels;
using System.Net.Mail;
using System.Net;

namespace LamConference.Repository{
    public class RegistrationRepository : ReferenceIDHandler, IRegistration, IMailService{

        private readonly Data.AppContext _context;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnv;

        public RegistrationRepository(Data.AppContext context,
            IConfiguration config,
            IWebHostEnvironment hostEnv
        ) : base(context)
        {
            _context = context;
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

        public async Task<bool> Registration(RegistrationViewModel viewModel)
        {
            // ModelHandler handler = new(){};
            // bool status = handler.RegistrationModel(viewModel);
            // if(status)
            // {
                // IDHandler.
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
                    Email = data.Email
                };

                //Big TODO::Add exception handler
                await _context.StudentData.AddAsync(data);
                _context.ReferenceIDs.Remove(isRefIDValid);
                await _context.SaveChangesAsync();

                SendMail(model);

                return true;
            // }
            return false;
        }

        public async  Task<bool> SendMail(MailHandlerModel model)
        {
            //Big TODO::Add exception handler.
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

            
            message.Subject = "Hello from Lam Conference";
            message.Body = mailBody;
            

            client.Send(message);


            return true;
        }
    }
}