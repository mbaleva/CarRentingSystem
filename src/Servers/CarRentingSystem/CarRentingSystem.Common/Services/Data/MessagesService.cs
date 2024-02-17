using CarRentingSystem.Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarRentingSystem.Common.Services.Data;

public class MessagesService : IMessagesService
{
    private DbContext _context;

    public MessagesService(DbContext context)
    {
        _context = context;
    }

    public async Task MarkMessageAsPublished(int id)
    {
        var message = await _context.FindAsync<Message>(id);
        message.Published = true;

        await _context.SaveChangesAsync();
    }

    public async Task Save<TEntity>(TEntity entity, params Message[] messages)
    {
        foreach (var item in messages)
        {
            _context.Add(item);
        }

        _context.Add(entity);

        await _context.SaveChangesAsync();
    }

    public async Task SaveMessages(params Message[] messages)
    {
        foreach (var item in messages)
        {
            _context.Add(item);
        }

        await _context.SaveChangesAsync();
    }
}
