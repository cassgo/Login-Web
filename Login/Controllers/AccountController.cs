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
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        // 登出并清除本地 Cookie
        [HttpPost]
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Login", "Account")
            }, OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
