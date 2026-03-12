using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _LayoutHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
