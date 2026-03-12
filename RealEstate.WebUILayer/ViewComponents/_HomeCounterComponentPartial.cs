using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeCounterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
