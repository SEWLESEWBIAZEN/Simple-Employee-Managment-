using MongoDB.Driver;
using EmployeeM.Models;
using Microsoft.Extensions.Options;

namespace EmployeeM.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _employeeCollection;

        public AuthService(
            IOptions<MongoDBSettings> authmongodbsettings)
        {
            var mongoClient = new MongoClient(
                authmongodbsettings.Value.ConnectionString);

            var Database = mongoClient.GetDatabase(
                authmongodbsettings.Value.DatabaseName);

            _employeeCollection = Database.GetCollection<User>(
               authmongodbsettings.Value.AuthCollectionName);
        }
        public async Task<User?> GetAsync(string username,string password) =>
        await _employeeCollection.Find(x => x.Username == username && x.Password==password).FirstOrDefaultAsync();
    }
}
