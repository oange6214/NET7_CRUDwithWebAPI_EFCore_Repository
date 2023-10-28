using FormulaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormulaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private static readonly List<Driver> _drivers = new()
    {
        new Driver()
        {
            Id = 1,
            Name = "Lewis Hamilton",
            Team = "Mercedes AMG F1",
            DriverNumber = 44
        },
        new Driver()
        {
            Id = 2,
            Name = "George Russel",
            Team = "Mercedes AMG F1",
            DriverNumber = 63
        },
        new Driver()
        {
            Id = 3,
            Name = "Sebastian Vettel",
            Team = "Austin Martin",
            DriverNumber = 5
        }
    };

    [HttpGet]
    public IActionResult GetAllDrivers()
    {
        return Ok(_drivers);
    }

    [HttpGet("{id}")]
    public IActionResult GetDriverById(int id)
    {
        var driver = _drivers.FirstOrDefault(x => x.Id == id);

        if (driver == null)
        {
            return NotFound();
        }

        return Ok(driver);
    }

    [HttpPost]
    public IActionResult AddDriver(Driver driver)
    {
        driver.Id = _drivers.Max(d => d.Id) + 1;
        _drivers.Add(driver);

        return CreatedAtAction(nameof(GetDriverById), new { id = driver.Id }, driver);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDriver(int id)
    {
        var driver = _drivers.FirstOrDefault(x => x.Id == id);
        if (driver == null)
        {
            return NotFound();
        }

        _drivers.Remove(driver);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateDriver(int id, Driver updatedDriver)
    {
        var existingDriver = _drivers.FirstOrDefault(x => x.Id == id);
        
        if (existingDriver == null)
        {
            return NotFound();
        }

        existingDriver.Name = updatedDriver.Name;
        existingDriver.Team = updatedDriver.Team;
        existingDriver.DriverNumber = updatedDriver.DriverNumber;

        return NoContent();
    }
}
