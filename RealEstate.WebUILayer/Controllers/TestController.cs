using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
