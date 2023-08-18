using System.Linq;
using LamConference.Services;
using LamConference.Models;
using Microsoft.EntityFrameworkCore;
using LamConference.ViewModel;
using LamConference.Handlers;

namespace LamConference.Repository{
    public class UserRepository : UserHandler, IUser{
        private readonly Data.AppContext _context;
        public UserRepository(Data.AppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ReferenceID>> GetAllID()
        {
            return await _context.ReferenceIDs.ToListAsync();
        }
        public List<ReferenceID> AvailableIDs()
        {
            return _context.ReferenceIDs.ToList();
        }

        public async Task<List<StudentData>> GetAllStudents()
        {
            return await _context.StudentData.ToListAsync();
        }

        public int GetTotalStudents()
        {
            return _context.StudentData.ToList().Count;
        }

        public int TotalGeneratedID()
        {
            var totalStudentData = _context.StudentData.ToList().Count;
            var totalReferenceID = _context.ReferenceIDs.ToList().Count;
 
            return totalStudentData + totalReferenceID;
        }

        public async Task<bool> FindRegisteredStudent(Guid id)
        {
            if(id != Guid.Empty)
            {
                var instance = await FindStudent(id);
                if(instance != null)
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        public async Task<bool> DeleteReferenceID(IDViewModel viewModel)
        {
            if(viewModel != null)
            {
                var RefID = await FindStudent(viewModel.RefID);
                if(RefID != null)
                {
                    //Big TODO::Add exception handler.
                    _context.StudentData.Remove(RefID);
                    await _context.SaveChangesAsync();

                    ReferenceID model = new(){
                        Id = RefID.RefId
                    };

                    //Big TODO::Add exception handler.
                    await _context.ReferenceIDs.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<List<DashboardViewModel>> DisplayProperties()
        {
            var studentDatas = await GetAllStudents();
            var avaliableRefID = AvailableIDs().Count;
            var totalGeneratedRefId =  TotalGeneratedID();
            var totalRegisteredStudents = GetTotalStudents();
            List<DashboardViewModel> model = new(){};
            foreach(var item in studentDatas)
            {
                DashboardViewModel testInstance = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Telephone = item.Telephone,
                    Department = item.Department,
                    Level = item.Level,
                    RefID = item.RefId,
                    AvailableRefID = avaliableRefID,
                    TotalGeneratedID = totalGeneratedRefId,
                    TotalRegisteredStudents = totalRegisteredStudents
                };

                model.Add(testInstance);
            }

            return model;
        }

    }
}