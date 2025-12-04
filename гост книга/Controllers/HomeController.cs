using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IRepository _repo;

    public HomeController(IRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var messages = _repo.GetMessages();
        return View(messages);
    }

    [HttpPost]
    public IActionResult AddMessage(string message)
    {
        var userId = HttpContext.Session.GetString("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        _repo.AddMessage(new GuestMessage
        {
            Id_User = int.Parse(userId),
            Message = message,
            MessageDate = DateTime.Now
        });

        _repo.Save();

        return RedirectToAction("Index");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}



