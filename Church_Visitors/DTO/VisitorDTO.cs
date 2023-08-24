using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church_Visitors.DTO
{
    public class VisitorDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string GuestOf { get; set; }
        public string OtherRemarks { get; set; }
        public DateTime DateEntered { get; set; }
    }
}
