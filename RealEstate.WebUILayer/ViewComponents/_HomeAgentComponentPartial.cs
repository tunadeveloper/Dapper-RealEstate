using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeAgentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
