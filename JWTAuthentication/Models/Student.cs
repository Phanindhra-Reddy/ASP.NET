using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JWTAuthentication.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string studentId {get; set;}
        [BsonElement("FirstName")]
        public string FirstName {get; set;}
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Gender")]
        public string Gender {get; set;}
        [BsonElement("Email")]
        public string Email {get; set;}

    }
}