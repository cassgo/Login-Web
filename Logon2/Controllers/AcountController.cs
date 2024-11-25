using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    /// <summary>
    /// 登录功能
    /// </summary>
    /// <param name="returnUrl">登录成功后跳转的地址</param>
    [HttpGet]
    public IActionResult Login(string returnUrl = "/")
    {
        // 使用 OpenIdConnect 触发登录流程
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "OpenIdConnect");
    }

    /// <summary>
    /// 注销功能，支持 GET 和 POST 请求
    /// </summary>
    [HttpGet]
    [HttpPost]
    public IActionResult Logout()
    {
        // 清除本地和 Keycloak 的登录状态，注销后重定向到首页
        return SignOut(new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme,
            "OpenIdConnect");
    }
}
