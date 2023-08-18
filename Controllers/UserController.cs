using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class UserController : Controller{
        private readonly IUser _service;
        private readonly IRegistration _registrationService;
        public UserController(IUser service)
        {
            _service = service;
        }

        public async Task<ActionResult> Dashboard()
        {
            var properties = await _service.DisplayProperties();
            return View(properties);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteReferenceID(Guid id)
        {
            IDViewModel model = new(){RefID = id};
            bool foundInstance = await _service.FindRegisteredStudent(id);

            if(id != Guid.Empty && foundInstance)
            {
                await _service.DeleteReferenceID(model);
                return RedirectToAction(nameof(Dashboard));
            }

            return RedirectToAction(nameof(Dashboard));
        }
    }
}