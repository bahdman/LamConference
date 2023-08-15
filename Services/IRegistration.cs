using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IRegistration{
        Task<bool> IdCheck(IDViewModel viewModel);//Big Todo:: add viewmodel for the id that would be supplied by user
        Task<bool> Registration(RegistrationViewModel viewModel);
    }
}