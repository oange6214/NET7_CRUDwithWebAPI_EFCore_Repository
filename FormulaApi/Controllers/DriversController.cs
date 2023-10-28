using FormulaApi.Data;
using FormulaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly ApiDbContext _context;

    public DriversController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDriversAsync()
    {
        var driver = await _context.Drivers.ToListAsync();

        return Ok(driver);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDriverById(int id)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

        if (driver == null)
        {
            return NotFound();
        }

        return Ok(driver);
    }

    [HttpPost]
    public async Task<IActionResult> AddDriver(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDriverById), new { id = driver.Id }, driver);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

        if (driver == null)
        {
            return NotFound();
        }

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateDriver(int id, Driver updatedDriver)
    {
        var existingDriver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
        
        if (existingDriver == null)
        {
            return NotFound();
        }

        existingDriver.Name = updatedDriver.Name;
        existingDriver.Team = updatedDriver.Team;
        existingDriver.DriverNumber = updatedDriver.DriverNumber;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
