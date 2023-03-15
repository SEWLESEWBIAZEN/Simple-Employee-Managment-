using EmployeeM.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeM.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Employee> _employeeCollection;

    public MongoDBService(
        IOptions<MongoDBSettings> mongodbsettings)
    {
        var mongoClient = new MongoClient(
            mongodbsettings.Value.ConnectionString);

        var Database = mongoClient.GetDatabase(
            mongodbsettings.Value.DatabaseName);

        _employeeCollection = Database.GetCollection<Employee>(
           mongodbsettings.Value.EmployeeCollectionName);
    }

    public async Task<List<Employee>> GetAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

    public async Task<Employee?> GetAsync(int id) =>
        await _employeeCollection.Find(x => x.ID == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Employee employee) =>
        await _employeeCollection.InsertOneAsync(employee);

    public async Task UpdateAsync(int id, Employee employee) =>
        await _employeeCollection.ReplaceOneAsync(x => x.ID == id, employee);

    public async Task RemoveAsync(int id) =>
        await _employeeCollection.DeleteOneAsync(x => x.ID == id);
}