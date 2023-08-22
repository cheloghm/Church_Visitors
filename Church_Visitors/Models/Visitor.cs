using Android.Graphics.Drawables;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church_Visitors.Models
{
    public class Visitor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Fullname")]
        public string FullName { get; set; }

        [BsonElement("GuestOf")]
        public string GuestOf { get; set; }

        [BsonElement("OtherRemarks")]
        public string OtherRemarks { get; set; }

        [BsonElement("DateEntered")]
        public DateTime DateEntered { get; set; }
    }
}
