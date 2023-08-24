using LamConference.Models;
using LamConference.HandlerModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LamConference.Handlers{
    public class UserHandler{
        private readonly Data.AppContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHandler(Data.AppContext context,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor
        ){
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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
            if(studentDatas.Count < 1 && avaliableRefID > 1)
            {
                DashboardHandlerModel instance = new(){
                    
                    AvailableRefID = avaliableRefID,
                    TotalGeneratedID = totalGeneratedRefId,
                    TotalRegisteredStudents = totalRegisteredStudents
                };

                model.Add(instance);
                return model;
            }
            foreach(var item in studentDatas)
            {
                DashboardHandlerModel instance = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
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

        public async Task<string> ReturnController()
        {
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

            var userRole = await _userManager.GetRolesAsync(user);
            //Remove unecessary usings

            return userRole[0];
        }
    }
}