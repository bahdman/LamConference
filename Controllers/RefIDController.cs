using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    [Authorize(Roles = "Finance, IT")]
    public class RefIDController : Controller{

        private readonly IIdGenerator _idService;
        private readonly IUser _userService;

        public RefIDController(IIdGenerator idService, IUser userService)
        {
            _idService = idService;
            _userService = userService;
        }

        public ActionResult GenerateID()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RefID(GenerateIDViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var instance = await _idService.IDGenerator(viewModel);
                if(instance)
                {
                    return RedirectToAction(nameof(ViewGeneratedIDs));//FiananceDashboard
                }
            }

            return View(viewModel);
        }


        public async Task<IActionResult> ViewGeneratedIDs()
        {
            return View(await _userService.GetAllReferenceID());
        }

        
    } 
}