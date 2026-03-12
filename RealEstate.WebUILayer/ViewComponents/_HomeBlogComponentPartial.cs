using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeBlogComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
