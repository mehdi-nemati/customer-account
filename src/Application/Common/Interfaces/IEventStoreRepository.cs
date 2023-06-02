using CustomerAccount.Domain.Common;

namespace CustomerAccount.Application.Common.Interfaces;
public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggregateId(int ItemId);
}
