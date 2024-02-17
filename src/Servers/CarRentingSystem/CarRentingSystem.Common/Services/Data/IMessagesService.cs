using CarRentingSystem.Common.Data.Models;
using System.Threading.Tasks;

namespace CarRentingSystem.Common.Services.Data;

public interface IMessagesService
{
    public Task Save<TEntity>(TEntity entity, params Message[] messages);
    
    Task SaveMessages(params Message[] messages);

    public Task MarkMessageAsPublished(int id);
}
