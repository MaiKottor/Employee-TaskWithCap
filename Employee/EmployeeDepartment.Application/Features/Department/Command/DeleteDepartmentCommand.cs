using EmployeeDepartment.Application.Features.Employee.Command;
using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Department.Command
{
   public class DeleteDepartmentCommand : IRequest<bool>
    {
        public int DepartmentId { get; }

        public DeleteDepartmentCommand(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;
        public DeleteDepartmentHandler(IRepository<Domain.Entities.Department> departmentRepository)
        {
            _deparmentRepository = departmentRepository;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            try {
                await _deparmentRepository.DeleteAsync(request.DepartmentId);

            }
            catch (Exception ex) {
                return false;
            }
            

            return true;
        }
    }

}
