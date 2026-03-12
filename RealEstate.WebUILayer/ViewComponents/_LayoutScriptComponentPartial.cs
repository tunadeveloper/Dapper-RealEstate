using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _LayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
