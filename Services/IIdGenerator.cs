using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IIdGenerator{
        Task<bool> IDGenerator(GenerateIDViewModel viewModel);
    }
}