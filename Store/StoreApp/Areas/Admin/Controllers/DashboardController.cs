using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")] //taghelperların ilgili yönlendirmeleri yapabilmesi için endpointlerle controllerları eşleştirmesi lazım
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}