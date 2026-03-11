using Microsoft.AspNetCore.Mvc;
using RealEstate.WebUILayer.Models;
using System.Diagnostics;

namespace RealEstate.WebUILayer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
