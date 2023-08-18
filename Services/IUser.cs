using LamConference.Models;
using LamConference.ViewModel;

namespace LamConference.Services{
    public interface IUser{
        Task<List<ReferenceID>> GetAllID();
        Task<List<StudentData>> GetAllStudents();
        int GetTotalStudents();
        Task<bool> FindRegisteredStudent(Guid id);
        Task<bool> DeleteReferenceID(IDViewModel viewModel);
        int TotalGeneratedID();
        Task<List<DashboardViewModel>> DisplayProperties();
    }
}