using EmployeeM.Models;
using EmployeeM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeM.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class EmployeeController : ControllerBase
{
    private readonly MongoDBService _mongodbservice;

    public EmployeeController(MongoDBService mongodbservice) =>
        _mongodbservice = mongodbservice;

    [HttpGet]
    
    public async Task<List<Employee>> Get() =>
        await _mongodbservice.GetAsync();

    [HttpGet("{id}")]
    
    public async Task<ActionResult<Employee>> Get(int id)
    {
        var employee = await _mongodbservice.GetAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        return employee;
    }

    
    [HttpPost]
    public async Task<IActionResult> Post(Employee employee)
    {
        await _mongodbservice.CreateAsync(employee);

        return CreatedAtAction(nameof(Get), new { id = employee.ID }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee updatedEmployee)
    {
        var employee = await _mongodbservice.GetAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        updatedEmployee.ID = employee.ID;

        await _mongodbservice.UpdateAsync(id, updatedEmployee);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _mongodbservice.GetAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        await _mongodbservice.RemoveAsync(id);

        return NoContent();
    }
}