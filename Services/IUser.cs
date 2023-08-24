using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IUser{
        Task<bool> FindRegisteredStudent(Guid id);
        Task<bool> DeleteReferenceID(IDViewModel viewModel);
        Task<List<RefIDViewModel>> GetAllReferenceID();
        Task<List<FinanceDashboardViewModel>> FinanceDisplayProperties();
        Task<List<ITDashboardViewModel>> ITDisplayProperties();
        Task<bool> SetEventPrice(PriceViewModel viewModel);
    }
}