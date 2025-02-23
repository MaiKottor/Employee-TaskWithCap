using DotNetCore.CAP;
using EmployeeDepartment.Application.Features.Employee.Command;
using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Features.Employee.Querey;
using EmployeeDepartment.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDepartment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICapPublisher _capBus;

        public EmployeeController(IMediator mediator, ICapPublisher capBus)
        {
            _mediator = mediator;
            _capBus = capBus;

        }
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployeeToDepartment([FromBody] AddEmployeeRequestDTO employeeRequest)
        {
            var command = new AddEmployeeCommand(employeeRequest);
            var employeeId = await _mediator.Send(command);
            // Publish event after employee is added
            await _capBus.PublishAsync("employee.added", new EmailEventDetails
            {
                Subject = "welcome on boarding",
                To = employeeRequest.Email,
                Body=$"welcome{employeeRequest.Name} in our componay"
            });
            return Ok(employeeId);
        }
        public class EmailEventDetails
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
        [HttpPost("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody]int id)
        {
            var command = new DeleteEmployeeCommand(id);
            var result = await _mediator.Send(command);

            if (result)
                return Ok(new {success=true, message = "Employee deleted successfully." });
            else
                return Ok(new { success = false, message = "Employee not found." });

        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            if (employee == null) return NotFound();
            return Ok(employee);
        }
        [HttpPost("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] AddEmployeeRequestDTO employee)
        {
            var command = new EditEmployeeCommand(employee,id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
