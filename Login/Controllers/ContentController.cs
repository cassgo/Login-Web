using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Authorize] // 确保页面受保护
    public class ContentController : Controller
    {
        public IActionResult Index()
        {
            var userName = User.Identity?.Name; // 获取当前登录用户
            return View(model: userName);
        }
    }
}
