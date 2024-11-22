using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginPageDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // 检查用户是否已经登录
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;  // 获取用户的名称
                                                    // 返回视图并显示用户名
                ViewBag.UserName = userName;
            }
            else
            {
                ViewBag.UserName = "未登录";
            }
            return View();
        }
    }


}
