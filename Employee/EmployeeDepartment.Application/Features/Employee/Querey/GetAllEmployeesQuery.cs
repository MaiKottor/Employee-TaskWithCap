using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Employee.Querey
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
    {
    }
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeService _employeeRepository;

        public GetAllEmployeesHandler(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return employees;
        }
    }
}
