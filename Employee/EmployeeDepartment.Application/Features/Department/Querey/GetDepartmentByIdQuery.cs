using EmployeeDepartment.Application.Features.Department.DTO;
using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Department.Querey
{

    public class GetDepartmentByIdQuery : IRequest<DepartmentDTO>
    {
        public int Id { get; set; }
        public GetDepartmentByIdQuery(int id) { Id = id; }
    }
    public class GeDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDTO>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;
        public GeDepartmentByIdHandler(IRepository<Domain.Entities.Department> departmentRepository, IEmployeeService empService)
        {
            _deparmentRepository = departmentRepository;
        }


        public async Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _deparmentRepository.GetByIdAsync(request.Id);
            return department == null ? null : new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name

            };
        }
    }
}
