using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// 添加 Razor 页面服务
builder.Services.AddControllersWithViews();

// 添加身份验证服务，使用 JWT Bearer 和 Keycloak 配置
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "CookieAuth"; // 默认使用 Cookie
    options.DefaultChallengeScheme = "oidc";          // 挑战时使用 OIDC
})
.AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "UserLoginCookie";
    options.LoginPath = "/Account/Login";
})
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "http://localhost:8080/realms/myrealm"; // Keycloak 服务器地址
    options.ClientId = "myclient";                              // Keycloak Client ID
    options.ClientSecret = "FCIXFynpcqvUJPIcnu3aO4goE1obvPp4";  // Keycloak Client Secret
    options.ResponseType = "code";                              // 使用 Authorization Code Flow
    options.GetClaimsFromUserInfoEndpoint = true;               // 从用户信息端点获取 Claims
    options.SaveTokens = true;                                  // 保存访问和刷新令牌

    // 配置 Token 验证
    options.TokenValidationParameters.ValidateAudience = true;
    options.TokenValidationParameters.ValidAudience = "myclient"; // 与 Keycloak Client ID 匹配
    options.TokenValidationParameters.ValidateIssuer = true;
    options.TokenValidationParameters.ValidIssuer = "http://localhost:8080/realms/myrealm";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 启用身份验证中间件
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
