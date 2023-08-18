using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IRegistration{
        Task<bool> IdCheck(IDViewModel viewModel);
        Task<bool> Registration(RegistrationViewModel viewModel);
    }
}