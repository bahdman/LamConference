using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LamConference.Repository{
    public class QueryRepository : IQuery
    {
        private readonly Data.AppContext _context;

        public QueryRepository(Data.AppContext context)
        {
            _context = context;
        }
        public async Task<List<QueryResultViewModel>> Search(string searchTerm)
        {
            List<QueryResultViewModel> modelItems = new();

            var items = from m in _context.StudentData select m;

            if(!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm.ToLower();
                items = items.Where(m => m.FirstName.Contains(searchTerm) || m.LastName.Contains(searchTerm));   
            }   

            await items.ToListAsync();

            foreach(var item in items)
            {
                QueryResultViewModel modelItem = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Department = item.Department,
                    Level = item.Level
                };

                modelItems.Add(modelItem);
            }        
            
            return modelItems;
        }
    }
}