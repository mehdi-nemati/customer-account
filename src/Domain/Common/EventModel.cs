using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerAccount.Domain.Common;
public class EventModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public int AggregateIdentifier { get; set; }
    public string EventType { get; set; }
    public BaseEvent EventData { get; set; }
}
