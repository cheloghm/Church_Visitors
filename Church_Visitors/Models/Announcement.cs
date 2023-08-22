using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Church_Visitors.Models
{
    public class Announcement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

        [BsonElement("DateCreated")]
        public DateTime DateCreated { get; set; }
    }
}
