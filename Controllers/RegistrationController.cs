using System.Text.RegularExpressions;
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
        public ActionResult Registration(Guid id)
        {
            if(id == Guid.Empty)
            {
                return RedirectToAction(nameof(Register));
            }
            //Big Todo:: get ref ID and return to view in the input so you can
            //pick it up for saving along with student info
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
        {
            // if(ModelState.IsValid)
            // {
                var instance = await _service.Registration(viewModel);
                if(instance)
                {
                    return RedirectToAction("ok", "ok");//Big Todo:: Redirect to success page
                }
            // }
            return View(viewModel);
        }
    } 
}