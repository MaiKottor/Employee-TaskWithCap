using EmployeeDepartment.Application.Features.Department.DTO;
using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Features.Employee.Querey;
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
   public class GetAllDepartmentsQuerey : IRequest<List<DepartmentDTO>>
    {
    }
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuerey, List<DepartmentDTO>>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;

        public GetAllDepartmentsHandler(IRepository<Domain.Entities.Department> deparmentRepository)
        {
            _deparmentRepository = deparmentRepository;
        }

        public async Task<List<DepartmentDTO>> Handle(GetAllDepartmentsQuerey request, CancellationToken cancellationToken)
        {
            var depts =  _deparmentRepository.GetAllAsync().Select(x=>new DepartmentDTO {
            Id=x.Id,Name=x.Name
            }).ToList();
            return depts;
        }
    }

}
