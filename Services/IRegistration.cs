using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IRegistration{
        Task<bool> IdCheck(IDViewModel viewModel);
        Task<bool> ValidateEmail(EmailViewModel model);
        Task<bool> Registration(RegistrationViewModel viewModel);
        Task<CheckRegViewModel> CheckCode(IDViewModel viewModel);
    }
}