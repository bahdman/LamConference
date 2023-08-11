using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IAccount
    {
        Task<bool> Register(RegisterViewModel viewModel, string role);
        Task<bool> Login(LoginViewModel viewModel);
    }
}