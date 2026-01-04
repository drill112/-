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

    [HttpGet]
    public IActionResult GetMessagesAjax()
    {
        var list = _repo.GetMessages()
            .Select(m => new {
                id = m.Id,
                userId = m.Id_User,
                text = m.Message,
                date = m.MessageDate.ToString("dd.MM.yyyy HH:mm:ss")
            });

        return Json(list);
    }

    [HttpPost]
    public IActionResult AddMessageAjax(string message)
    {
        var userId = HttpContext.Session.GetString("UserId");

        if (userId == null)
            return Json(new { success = false, error = "Пользователь не авторизован" });

        var msg = new GuestMessage
        {
            Message = message,
            MessageDate = DateTime.Now,
            Id_User = int.Parse(userId)
        };

        _repo.AddMessage(msg);
        _repo.Save();

        return Json(new { success = true });
    }

}



