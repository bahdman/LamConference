using LamConference.Handlers;
using LamConference.Models;
using LamConference.Services;
using LamConference.ViewModel;

namespace LamConference.Repository{
    public class RegistrationRepository : IDHandler, IRegistration{

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

                await _context.StudentData.AddAsync(data);
                _context.ReferenceIDs.Remove(test);
                await _context.SaveChangesAsync();

                return true;
            }
            //check if regId is valid(IN DB) before registering user;
            return false;
        }
    }
}