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

        public RegistrationRepository(Data.AppContext context) : base(context)
        {
            _context = context;
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
            ModelHandler handler = new(){};
            bool status = handler.RegistrationModel(viewModel);
            if(status)
            {
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
            }
            return false;
        }

        public bool SendMail(MailHandlerModel model)
        {
            //Big TODO::Add exception handler.
            using var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("anetoroselumese@gmail.com", "nqsskzwcnlcrjqrq");
            using var message = new MailMessage(
                from: new MailAddress("anetoroselumese@gmail.com", "LAM Conference"),
                to: new MailAddress(model.Email, model.Name)
                );

            message.Subject = "Hello from Lam Conference";
            message.Body = @"Registration was successful
            Thanks for your interest in the conference.

            Regards,
            Anetor.
            ";
            

            client.Send(message);


            return true;
        }
    }
}