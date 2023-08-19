using LamConference.Models;
using LamConference.HandlerModels;
using Microsoft.EntityFrameworkCore;

namespace LamConference.Handlers{
    public class UserHandler{
        private readonly Data.AppContext _context;
        public UserHandler(Data.AppContext context)
        {
            _context = context;
        }

        public async Task<StudentData> FindStudent(Guid id)
        {
            //Big TODO::Add exception handler.
            return await _context.StudentData.Where(m => m.RefId == id).FirstOrDefaultAsync();
        }

        public int AvailableIDs()
        {
            return _context.ReferenceIDs.ToList().Count;
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

        public async Task<List<DashboardHandlerModel>> DisplayProperties()
        {
            var studentDatas = await GetAllStudents();
            var avaliableRefID = AvailableIDs();
            var totalGeneratedRefId =  TotalGeneratedID();
            var totalRegisteredStudents = GetTotalStudents();
            List<DashboardHandlerModel> model = new(){};
            foreach(var item in studentDatas)
            {
                DashboardHandlerModel instance = new(){
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

                model.Add(instance);
            }
            return model;
        }
    }
}