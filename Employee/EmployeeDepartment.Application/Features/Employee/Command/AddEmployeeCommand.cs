using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Employee.Command
{
   public class AddEmployeeCommand :IRequest<int>
    {
        public AddEmployeeRequestDTO _request { get; set; }
        public AddEmployeeCommand(AddEmployeeRequestDTO request) {
            _request = request;
        }

    }
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, int>
    {
        private readonly IEmployeeService _service;

        public AddEmployeeHandler(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeId = await _service.AddEmployee(request._request);

            return employeeId;
        }
    }
}
