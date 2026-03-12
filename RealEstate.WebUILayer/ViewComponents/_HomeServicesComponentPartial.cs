using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeServicesComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
