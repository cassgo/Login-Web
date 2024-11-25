using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// 添加身份验证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = "http://localhost:8080/realms/myrealm"; // Keycloak 地址
    options.ClientId = "Login2"; // 新的 Client 名称
    options.ClientSecret = "T2sHscEm4saufQEaBA8OfJmkA0HBQMFN"; // 新的 Client Secret
    options.ResponseType = OpenIdConnectResponseType.Code; // 使用 Authorization Code Flow
    options.SaveTokens = true; // 保存令牌
    options.GetClaimsFromUserInfoEndpoint = true; // 从 UserInfo 获取用户信息
    options.RequireHttpsMetadata = false; // 禁用 HTTPS 元数据限制（仅开发环境）
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/realms/myrealm",
        ValidateAudience = true,
        ValidAudience = "Login2",
        ValidateLifetime = true
    };
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// 配置中间件
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();