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
    public class AddDepartmentCommand : IRequest<int>
    {
        public string name { get; set; }
        public AddDepartmentCommand(string _name)
        {
            name = _name;
        }

    }
    public class AddDepartmentHandler : IRequestHandler<AddDepartmentCommand, int>
    {
        private readonly IRepository<Domain.Entities.Department> _deparmentRepository;

        public AddDepartmentHandler(IRepository<Domain.Entities.Department> deparmentRepository)
        {
            _deparmentRepository = deparmentRepository;
        }

        public async Task<int> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var dept = new Domain.Entities.Department(request.name); 
             await _deparmentRepository.AddAsync(dept);
            await _deparmentRepository.SaveAsync();
            return dept.Id;
        }
    }


}
