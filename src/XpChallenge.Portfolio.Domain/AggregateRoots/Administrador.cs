using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace XpChallenge.Portfolio.Domain.AggregateRoots
{
    public class Administrador(string email)
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string Email { get; set; } = email;
    }
}
