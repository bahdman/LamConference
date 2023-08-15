using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class RefIDController : Controller{

        private readonly IIdGenerator _service;

        public RefIDController(IIdGenerator service)
        {
            _service = service;
        }

        public ActionResult GenerateID()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RefID(GenerateIDViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var instance = await _service.IDGenerator(viewModel);
                if(instance)
                {
                    return RedirectToAction("success", "ok");//FiananceDashboard
                }
            }

            return View(viewModel);
        }

        
    } 
}