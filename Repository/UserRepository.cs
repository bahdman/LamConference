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

        public async Task<List<ReferenceID>> GetAllReferenceID()//
        {
            return await _context.ReferenceIDs.ToListAsync();
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
            foreach(var item in displayProperties)
            {
                FinanceDashboardViewModel instance = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Telephone = item.Telephone,
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
            var estimatedAmount = "FROM DB"; //Big TODO:: would come straight from the DB.
            List<ITDashboardViewModel> model = new(){};
            foreach(var item in displayProperties)
            {
                ITDashboardViewModel instance = new(){
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Telephone = item.Telephone,
                    Department = item.Department,
                    Level = item.Level,
                    RefID = item.RefID,
                    AvailableRefID = item.AvailableRefID,
                    TotalGeneratedID = item.TotalGeneratedID,
                    TotalRegisteredStudents = item.TotalRegisteredStudents,
                    EstimatedAmount = 3000000
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