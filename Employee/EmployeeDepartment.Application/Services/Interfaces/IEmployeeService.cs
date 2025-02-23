using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Services.Interfaces
{
   public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<int> AddEmployee(AddEmployeeRequestDTO request);
        Task<Department> GetDeparmentByEmployeeId(int employeeid);
        Task<bool> EditEmployee(int id, AddEmployeeRequestDTO request);



    }
}
