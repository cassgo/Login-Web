using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            ViewBag.Message = $"Welcome, {User.Identity.Name}!";
        }
        else
        {
            ViewBag.Message = "Please log in.";
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
