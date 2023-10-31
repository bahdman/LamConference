using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
                this.ModelState.AddModelError("RefID", "Invalid Reference ID");
            }
            return View(viewModel);
        }


        //Registration:: Collect user data.
        public async Task<ActionResult> Registration(Guid id)
        {
            if(id != Guid.Empty)
            {
                IDViewModel model = new(){RefID = id};
                bool instance = await _service.IdCheck(model);

                if(instance)
                {                    
                    RegistrationViewModel viewModel = new(){
                        RefID = id
                    };
                    return View(viewModel);
                } 
                return RedirectToAction(nameof(Register));               
            }

            return RedirectToAction(nameof(Register));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                EmailViewModel model = new(){
                    Email = viewModel.Email
                };
                var emailValid = await _service.ValidateEmail(model);
                if(!emailValid)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(viewModel);
                }
                var instance = await _service.Registration(viewModel);
                if(instance)
                {
                    return RedirectToAction(nameof(Success));
                }
            }
            return View(viewModel);
        }

        public ActionResult CheckCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidateCode(IDViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _service.CheckCode(model);
                return View(response);
            }

            return View(model);
            
        }

        public ActionResult Success()
        {
            return View();
        }
    } 
}