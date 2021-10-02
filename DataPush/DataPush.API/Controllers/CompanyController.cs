using Microsoft.AspNetCore.Mvc;

namespace DataPush.API.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}