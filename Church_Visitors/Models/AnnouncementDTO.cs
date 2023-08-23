using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Church_Visitors.Models
{
    public class AnnouncementDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
