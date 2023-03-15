namespace EmployeeM.Models
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string EmployeeCollectionName { get; set; } = null!;
         public string AuthCollectionName { get; set; } = null!;
    }
}
