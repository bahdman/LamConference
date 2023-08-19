using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    [Authorize(Roles = "Finance")]
    public class FinanceController : Controller{
        private readonly IUser _service;
        public FinanceController(IUser service)
        {
            _service = service;
        }


        public async Task<ActionResult> Dashboard()
        {
            var properties = await _service.FinanceDisplayProperties();
            return View(properties);
        }
    }
}