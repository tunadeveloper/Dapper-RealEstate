using Microsoft.AspNetCore.Mvc;
using RealEstate.WebUILayer.Models;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _PageHeroComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string breadcrumbCurrent)
        {
            var model = new PageHeroViewModel
            {
                Title = title,
                BreadcrumbCurrent = breadcrumbCurrent
            };
            return View(model);
        }
    }
}
