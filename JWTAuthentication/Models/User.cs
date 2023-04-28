using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JWTAuthentication.Models
{
    public class User
    {
        [BsonId]
        [BsonElement("EmailAddress")]
        public string EmailAddress { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
    }
}