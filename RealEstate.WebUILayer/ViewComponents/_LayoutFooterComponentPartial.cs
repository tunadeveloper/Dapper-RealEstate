using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _LayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
