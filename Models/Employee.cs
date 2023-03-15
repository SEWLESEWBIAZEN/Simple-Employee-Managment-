using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeM.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int ID { get; set; }


    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Phone { get; set; }=null!;
    public string Role { get; set; }

    public int IsActive { get; set; }

    public Employee(int id,string name,int age,int isActive,string phone,string role )
    {
        this.ID = id;
        this.Name = name;
        this.Age = age;
        this.IsActive = isActive;
        this.Phone = phone;
        this.Role= role;    

    }
}