using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeAboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
