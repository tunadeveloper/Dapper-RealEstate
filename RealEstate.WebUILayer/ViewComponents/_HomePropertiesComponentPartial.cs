using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomePropertiesComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
