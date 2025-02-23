using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Domain.Entities;
using EmployeeDepartment.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Employee.Command
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int EmployeeId { get; }

        public DeleteEmployeeCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;
        private readonly IEmployeeService _empService;
        public DeleteEmployeeHandler(IRepository<Domain.Entities.Department> departmentRepository, IEmployeeService empService)
        {
            _deparmentRepository = departmentRepository;
            _empService = empService;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Find the department that contains the employee
            var department = await _empService.GetDeparmentByEmployeeId(request.EmployeeId);

            if (department == null)
                return false;

            // Remove the employee through the Aggregate Root
            var removed = department.RemoveEmployee(request.EmployeeId);

            if (!removed)
                return false;

            await _deparmentRepository.SaveAsync();
            return true;
        }
    }
}
