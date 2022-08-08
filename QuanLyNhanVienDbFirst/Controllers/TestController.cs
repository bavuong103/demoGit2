using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanVienDbFirst.Controllers
{
    public class TestController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
