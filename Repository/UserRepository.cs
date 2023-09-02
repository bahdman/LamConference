using LamConference.Services;
using LamConference.Models;
using Microsoft.EntityFrameworkCore;
using LamConference.ViewModel;
using LamConference.Handlers;
using Microsoft.AspNetCore.Identity;


namespace LamConference.Repository{
    public class UserRepository : UserHandler, IUser{
        private readonly Data.AppContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(Data.AppContext context,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor
        ) : base(context, userManager, httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<RefIDViewModel>> GetAllReferenceID()//
        {
            string returnController = await ReturnController();
            // int 
            var iDs =  await _context.ReferenceIDs.ToListAsync();
            List<RefIDViewModel> listInstance = new(){};
            if(iDs.Count < 1)
            {
                RefIDViewModel instance = new()
                {   
                    ReturnController = returnController,
                };

                listInstance.Add(instance);

                return listInstance;
            }
            
            foreach(var item in iDs)
            {
                RefIDViewModel instance = new()
                {
                    ID = item.Id,
                    ReturnController = returnController,
                    AvailableIDs = iDs.Count
                };

                listInstance.Add(instance);
            }

            return listInstance;
        }

        public async Task<bool> FindRegisteredStudent(Guid id)//
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

        public async Task<bool> DeleteReferenceID(IDViewModel viewModel)//
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

        public async Task<List<FinanceDashboardViewModel>> FinanceDisplayProperties()
        {
            var displayProperties = await DisplayProperties();
            List<FinanceDashboardViewModel> model = new(){};
            if(displayProperties.Count < 1)
            {
                FinanceDashboardViewModel instance = new()
                {
                    AvailableRefID = 0,
                    TotalGeneratedID = 0,
                    TotalRegisteredStudents = 0
                };

                model.Add(instance);
                return model;
            }
            foreach(var item in displayProperties)
            {
                FinanceDashboardViewModel instance = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Department = item.Department,
                    Level = item.Level,
                    RefID = item.RefID,
                    AvailableRefID = item.AvailableRefID,
                    TotalGeneratedID = item.TotalGeneratedID,
                    TotalRegisteredStudents = item.TotalRegisteredStudents
                };

                model.Add(instance);
            }

            return model;
        }

        public async Task<List<ITDashboardViewModel>> ITDisplayProperties()
        {
            var displayProperties = await DisplayProperties();
            var estimatedAmount = displayProperties[0].TotalRegisteredStudents * 1000; //Big TODO:: would come straight from the DB.
            List<ITDashboardViewModel> model = new(){};
            if(displayProperties.Count < 1)
            {
                ITDashboardViewModel instance = new(){
                    AvailableRefID = 0,
                    TotalGeneratedID = 0,
                    TotalRegisteredStudents = 0,
                    EstimatedAmount = 0
                };

                model.Add(instance);
                return model;
            }
            foreach(var item in displayProperties)
            {
                ITDashboardViewModel instance = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Department = item.Department,
                    Level = item.Level,
                    RefID = item.RefID,
                    AvailableRefID = item.AvailableRefID,
                    TotalGeneratedID = item.TotalGeneratedID,
                    TotalRegisteredStudents = item.TotalRegisteredStudents,
                    EstimatedAmount = estimatedAmount
                };

                model.Add(instance);
            }

            return model;
        }

        public async Task<bool> SetEventPrice(PriceViewModel viewModel)
        {
            //Big TODO:: Use RegEx to validate the price ==> .00 should be optional.
            return false;
        }
    }
}