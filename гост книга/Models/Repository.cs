using System.Collections.Generic;
using System.Linq;

public class Repository : IRepository
{
    private readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<GuestMessage> GetMessages()
    {
        return _db.Messages
            .OrderByDescending(m => m.MessageDate)
            .ToList();
    }

    public void AddMessage(GuestMessage message)
    {
        _db.Messages.Add(message);
    }

    public User? GetUser(string login, string password)
    {
        return _db.Users
            .FirstOrDefault(u => u.Name == login && u.Pwd == password);
    }

    public void AddUser(User user)
    {
        _db.Users.Add(user);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}

