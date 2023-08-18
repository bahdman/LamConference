using LamConference.Models;
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
    }
}