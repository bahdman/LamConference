using LamConference.Models;
using LamConference.ViewModel;

namespace LamConference.Handlers{
    public class ReferenceIDHandler{
        private readonly Data.AppContext _context;
        public ReferenceIDHandler(Data.AppContext context)
        {
            _context = context;
        }

        public async Task<ReferenceID> FindID(Guid id)
        {
            //Big TODO::Add exception handler.
            return await _context.ReferenceIDs.FindAsync(id);
        }

        public async Task<List<ReferenceID>> GetReferenceID(GenerateIDViewModel viewModel)
        {
            List<ReferenceID> listItem = new(){};
            if(viewModel.Value > 0)
            {                
                for(int i=0; i<viewModel.Value; i++)
                {
                    Guid id = Guid.NewGuid();
                    var isValid = await FindID(id);
                    if(isValid == null)
                    {
                        ReferenceID instance = new (){
                            Id = id
                        };

                    listItem.Add(instance);
                    }
                    else if(isValid != null)
                    {
                        i-=1; 
                    }                                 
                }
                return listItem;
            }
            return listItem;
        }
    }
}