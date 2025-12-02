using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _db;

    public AccountController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string login, string password)
    {
        var user = _db.Users
            .FirstOrDefault(u => u.Name == login && u.Pwd == password);

        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Неверный логин или пароль";
        return View();
    }

    public IActionResult Guest()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
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

        _db.Users.Add(new User { Name = login, Pwd = password });
        _db.SaveChanges();

        return RedirectToAction("Login");
    }
}

