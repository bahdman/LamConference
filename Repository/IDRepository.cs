using LamConference.Services;
using LamConference.Models;
using LamConference.Data;
using LamConference.ViewModel;
using LamConference.Handlers;

namespace LamConference.Repository{
    public class IDRepository : IDHandler, IIdGenerator{

        private readonly Data.AppContext _context;

        public IDRepository(Data.AppContext context) : base(context)
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

                    await _context.ReferenceIDs.AddAsync(instance);    
                    await _context.SaveChangesAsync();  
                }

                // for(int i=0; i<IDListHelper.Count; i++)
                // {
                //     ReferenceID instance = new (){
                //         Id = Guid.NewGuid()
                //     };

                //     await _context.ReferenceIDs.AddAsync(instance);    
                //     await _context.SaveChangesAsync();      
                // }

                return true;
            }
            return false;
        }
    }
}