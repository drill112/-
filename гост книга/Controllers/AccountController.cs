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
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Введите логин и пароль!";
            return View();
        }

        var user = _repo.GetUser(login, password);

        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Неверный логин или пароль!";
        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(string login, string password, string confirm)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            ViewBag.Error = "Введите логин!";
            return View();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Введите пароль!";
            return View();
        }

        if (password != confirm)
        {
            ViewBag.Error = "Пароли не совпадают!";
            return View();
        }

        var newUser = new User
        {
            Name = login,
            Pwd = password
        };

        _repo.AddUser(newUser);
        _repo.Save();

        return RedirectToAction("Login");
    }


    public IActionResult Guest()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}



