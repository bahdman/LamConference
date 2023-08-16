using LamConference.Handlers;
using LamConference.Models;
using LamConference.Services;
using LamConference.ViewModel;
using LamConference.HandlerModels;
using System.Net.Mail;
using System.Net;

namespace LamConference.Repository{
    public class RegistrationRepository : IDHandler, IRegistration, IMailService{

        private readonly Data.AppContext _context;

        public RegistrationRepository(Data.AppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IdCheck(IDViewModel viewModel)
        {
            if(viewModel != null)
            {
                var check = await FindID(viewModel.RefID);//Test
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
            if(status)//Proper validation::Use handler to validate the model information
            {
                // IDHandler.
                var test = await FindID(viewModel.RefID);//Test
                if(test == null || test.Id == Guid.Empty)
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

                await _context.StudentData.AddAsync(data);
                _context.ReferenceIDs.Remove(test);
                await _context.SaveChangesAsync();

                SendMail(model);

                return true;
            }
            //check if regId is valid(IN DB) before registering user;
            return false;
        }

        public bool SendMail(MailHandlerModel model)
        {
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