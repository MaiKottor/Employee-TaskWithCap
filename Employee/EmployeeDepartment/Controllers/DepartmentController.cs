using EmployeeDepartment.Application.Features.Department.Command;
using EmployeeDepartment.Application.Features.Department.DTO;
using EmployeeDepartment.Application.Features.Department.Querey;
using EmployeeDepartment.Application.Features.Employee.Command;
using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Features.Employee.Querey;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDepartment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllDepartments")]
        public async Task<ActionResult<List<DepartmentDTO>>> GetAllDepartments()
        {
            var depts = await _mediator.Send(new GetAllDepartmentsQuerey());
            return Ok(depts);
        }
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentRequest request)
        {
            var command = new AddDepartmentCommand(request.Name);
            var deptId = await _mediator.Send(command);
            return Ok(deptId);
        }
        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _mediator.Send(new GetDepartmentByIdQuery(id));
            if (department == null) return NotFound();
            return Ok(department);
        }
        [HttpPost("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment([FromBody] int id)
        {
            var command = new DeleteDepartmentCommand(id);
            var result = await _mediator.Send(command);

            if (result)
                return Ok(new { success = true, message = "Department deleted successfully." });
            else
                return Ok(new { success = false, message = "Department not found." });

        }
        [HttpPost("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO employee)
        {
            var command = new EditDepartmentCommand(employee, id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }

}
