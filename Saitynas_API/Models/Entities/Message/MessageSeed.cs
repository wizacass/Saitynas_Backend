using System.Linq;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Message;

public class MessageSeed : ISeed
{
    private readonly ApiContext _context;

    public MessageSeed(ApiContext context)
    {
        _context = context;
    }

    public void EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var message = new Message
        {
            Id = 1,
            Text = "Welcome to Virtual Clinic!"
        };

        _context.Messages.Add(message);
        _context.SaveChanges();
    }

    private bool ShouldSeed()
    {
        var message = _context.Messages.FirstOrDefault(m => m.Id == 1);
        return message == null;
    }
}
