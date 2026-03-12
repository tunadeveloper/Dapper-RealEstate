using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeClientComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
