using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace LoginPageDemo.Controllers
{
    public class AccountController : Controller
    {
        // 登录重定向到 Keycloak
        [HttpGet]
        public IActionResult Login()
        {
            // 通过 Challenge 重定向到 Keycloak 进行身份验证
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")  // 登录成功后跳转到 Home 页面
            }, OpenIdConnectDefaults.AuthenticationScheme);  // 使用 OpenIdConnect 进行身份验证
        }

        // 登出并清除本地 Cookie
        [HttpPost]
        public IActionResult Logout()
        {
            // 清除登录时创建的 Cookie 和 Keycloak 会话
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Login", "Account")  // 登出后跳转到 Login 页面
            }, OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // 这里是登录后的页面显示用户名的示例
        [HttpGet]
        public IActionResult Profile()
        {
            // 获取用户的用户名并传递给视图
            var userName = User.Identity.IsAuthenticated ? User.Identity.Name : "未登录";
            ViewBag.UserName = userName;
            return View();
        }
    }
}
