using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeDepartment.Application.Features.Employee.Command
{
   public class EditEmployeeCommand :IRequest<bool>
    {
        public int id { get; set; }
        public AddEmployeeRequestDTO request { get; set; }
        public EditEmployeeCommand(AddEmployeeRequestDTO emp, int employeeId)
        {
            id = employeeId;
            request = new AddEmployeeRequestDTO
            {
                DepartmentId = emp.DepartmentId,
                Name = emp.Name,
                Email = emp.Email
            };
        }
    }
    public class UpdateEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, bool>
    {
        private readonly IEmployeeService _service;

        public UpdateEmployeeCommandHandler(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.EditEmployee(request.id,request.request);

            return result;

        }
    }
}
