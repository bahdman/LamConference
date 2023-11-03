using LamConference.Services;
using LamConference.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class QueryController : Controller
    {
        private readonly IQuery _queryService;

        public QueryController(IQuery queryService)
        {
            _queryService = queryService;
        }

        public async Task<ActionResult> Search(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;
            
            var response = await _queryService.Search(searchTerm);

            return View(response);
        }
    }
}