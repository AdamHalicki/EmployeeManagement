using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/1.0/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDTO)
    {
        try
        {
            var employee = _employeeService.AddEmployee(employeeDTO.LastName, employeeDTO.Gender);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(Guid id)
    {
        var employee = _employeeService.FindEmployeeById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(Guid id, [FromBody] EmployeeDTO employeeDTO)
    {
        try
        {
            var employee = _employeeService.UpdateEmployee(id, employeeDTO.LastName, employeeDTO.Gender);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}