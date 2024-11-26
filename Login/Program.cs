using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// 添加 Razor 页面和身份验证服务
builder.Services.AddControllersWithViews();

// 配置身份验证
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "http://localhost:8080/realms/myrealm"; // 使用 http 协议
    options.ClientId = "myclient";
    options.ClientSecret = "FCIXFynpcqvUJPIcnu3aO4goE1obvPp4";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");

    // 禁用 HTTPS 要求，开发环境中可以用 HTTP
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();

// 配置中间件
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 开发环境中显示异常页面
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 启用身份验证中间件
app.UseAuthorization(); // 启用授权中间件

// 配置默认路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 配置回调路由
app.MapGet("/callback", (HttpContext context) =>
{
    // 获取回调中的授权码
    var code = context.Request.Query["code"];
    if (!string.IsNullOrEmpty(code))
    {
        // 在这里使用授权码获取 Token
        return Results.Ok($"Received authorization code: {code}");
    }
    return Results.BadRequest("Authorization code missing.");
});

app.Run();
