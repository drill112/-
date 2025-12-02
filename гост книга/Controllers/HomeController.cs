using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var messages = _db.Messages
            .OrderByDescending(m => m.MessageDate)
            .ToList();

        return View(messages);
    }

    [HttpPost]
    public IActionResult AddMessage(string message)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        _db.Messages.Add(new GuestMessage
        {
            Id_User = int.Parse(HttpContext.Session.GetString("UserId")),
            Message = message,
            MessageDate = DateTime.Now
        });

        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}


