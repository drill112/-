using System.Collections.Generic;

public interface IRepository
{
    IEnumerable<GuestMessage> GetMessages();
    void AddMessage(GuestMessage message);

    User? GetUser(string login, string password);
    void AddUser(User user);

    void Save();
}

