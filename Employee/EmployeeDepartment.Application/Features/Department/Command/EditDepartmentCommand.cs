using EmployeeDepartment.Application.Features.Department.DTO;
using EmployeeDepartment.Application.Features.Employee.Command;
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

namespace EmployeeDepartment.Application.Features.Department.Command
{
   public class EditDepartmentCommand : IRequest<bool>
    {
        public int id { get; set; }
        public DepartmentDTO request { get; set; }
        public EditDepartmentCommand(DepartmentDTO dept, int departmentId)
        {
            id = departmentId;
            request = new DepartmentDTO
            {
                Name = dept.Name,
            };
        }
    }
    public class UpdateDepartmentCommandHandler : IRequestHandler<EditDepartmentCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;

        public UpdateDepartmentCommandHandler(IRepository<Domain.Entities.Department> departmentRepository)
        {
            _deparmentRepository = departmentRepository;
        }

        public async Task<bool> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _deparmentRepository.GetByIdAsync(request.id);
            department.UpdateName( request.request.Name); 
            
           await  _deparmentRepository.UpdateAsync(department);
            return true;

        }
    }

}
