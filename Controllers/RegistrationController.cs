using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class RegistrationController : Controller{

        private readonly IRegistration _service;

        public RegistrationController(IRegistration service)
        {
            _service = service;
        }

        //Register:: Checks Ref ID for validaity
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(IDViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var instance = await _service.IdCheck(viewModel);
                if(instance)
                {
                    return RedirectToAction("Registration", new{id = viewModel.RefID});
                }
            }
            return View(viewModel);
        }


        //Registration:: Collect user data.
        public async Task<ActionResult> Registration(Guid id)
        {
            IDViewModel model = new(){RefID = id};
            bool instance = await _service.IdCheck(model);

            if(id == Guid.Empty && !instance)
            {
                return RedirectToAction(nameof(Register));
            }

            RegistrationViewModel viewModel = new(){
                RefID = id
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
        {
                var instance = await _service.Registration(viewModel);
                if(instance)
                {
                    return RedirectToAction(nameof(Success));
                }
            // }
            return View(viewModel);
        }

        public ActionResult Success()
        {
            return View();
        }
    } 
}