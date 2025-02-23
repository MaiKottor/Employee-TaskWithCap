using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Domain.Entities;
using EmployeeDepartment.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Employee.Querey
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeResponseDTO>
    {
        public int Id { get; set; }
        public GetEmployeeByIdQuery(int id) { Id = id; }
    }
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponseDTO>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;
        private readonly IEmployeeService _empService;
        public GetEmployeeByIdHandler(IRepository<Domain.Entities.Department> departmentRepository, IEmployeeService empService)
        {
            _deparmentRepository = departmentRepository;
            _empService = empService;
        }
      

        public async Task<EmployeeResponseDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _empService.GetDeparmentByEmployeeId(request.Id);
            var employee= department?.Employees.FirstOrDefault(e => e.Id == request.Id);
            return employee == null ? null : new EmployeeResponseDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                 DepartmentId= employee.DepartmentId
            };
        }
    }
}
