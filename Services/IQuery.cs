using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IQuery
    {
        Task<List<QueryResultViewModel>> Search(string searchTerm);
    }
}