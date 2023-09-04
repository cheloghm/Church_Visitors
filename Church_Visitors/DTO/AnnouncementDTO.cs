using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Church_Visitors.DTO
{
    public class AnnouncementDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
