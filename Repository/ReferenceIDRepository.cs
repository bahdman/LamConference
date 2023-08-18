using LamConference.Services;
using LamConference.Models;
using LamConference.Data;
using LamConference.ViewModel;
using LamConference.Handlers;

namespace LamConference.Repository{
    public class ReferenceIDRepository : ReferenceIDHandler, IIdGenerator{

        private readonly Data.AppContext _context;

        public ReferenceIDRepository(Data.AppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> IDGenerator(GenerateIDViewModel viewModel)
        {
            if(viewModel != null && viewModel.Value >= 1)
            {
                var IDListHelper = await GetReferenceID(viewModel);
                
                foreach(var item in IDListHelper)
                {
                    ReferenceID instance = new (){
                        Id = item.Id
                    };

                    //Big TODO::Add exception handler.
                    await _context.ReferenceIDs.AddAsync(instance);    
                    await _context.SaveChangesAsync();  
                }
                return true;
            }
            return false;
        }
    }
}