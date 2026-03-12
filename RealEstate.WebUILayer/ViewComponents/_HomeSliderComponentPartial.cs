using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeSliderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
