using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebUILayer.ViewComponents
{
    public class _HomeWorkflowComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
