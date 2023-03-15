using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace EmployeeM.Models
{
    
    public class User
    {
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
