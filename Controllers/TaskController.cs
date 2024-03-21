using Microsoft.AspNetCore.Mvc;

namespace SoftiusTestTask.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index() => View();
    }
}
