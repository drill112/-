using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IRepository _repo;

    public AccountController(IRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string login, string password)
    {
        var user = _repo.GetUser(login, password);

        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Неверный логин или пароль";
        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(string login, string password, string confirm)
    {
        if (password != confirm)
        {
            ViewBag.Error = "Пароли не совпадают!";
            return View();
        }

        _repo.AddUser(new User
        {
            Name = login,
            Pwd = password
        });

        _repo.Save();

        return RedirectToAction("Login");
    }

    public IActionResult Guest()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}


