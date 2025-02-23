using EmployeeDepartment.Application.Features.Employee.DTO;
using EmployeeDepartment.Application.Services.Interfaces;
using EmployeeDepartment.Domain.Entities;
using EmployeeDepartment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCore.CAP;
using EmployeeDepartment.Application.Features.Department.DTO;  // Required for Include and other EF Core features

namespace EmployeeDepartment.Application.Services.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IRepository<Department> _deparmentRepository;

        public EmployeeService( IRepository<Department> deparmentRepository)
        {
            _deparmentRepository = deparmentRepository;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var departments = await _deparmentRepository.GetAllAsync().Include(d => d.Employees)
            .ToListAsync(); 

            var employees = departments
                .SelectMany(dept => dept.Employees.Select(emp => new EmployeeDto
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Email = emp.Email,
                    DepartmentName = dept.Name  
                }))
                .ToList();
            return employees;
        }
        public async Task<int> AddEmployee(AddEmployeeRequestDTO request) {

            var department = await _deparmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
                throw new Exception("Department not found!");
            var employee = department.AddEmployee(request.Name, request.Email);
            await _deparmentRepository.SaveAsync();
          
            return employee.Id;
        }
        public async Task<bool> EditEmployee(int id,AddEmployeeRequestDTO request)

        {

            var department = await GetDeparmentByEmployeeId(id);
            if (department == null)
                throw new Exception("Department not found!");
            var employee = department.Employees.FirstOrDefault(e => e.Id == id);
            employee.UpdateDetails(request.Name, request.Email,request.DepartmentId);
            await _deparmentRepository.UpdateAsync(department);
            await _deparmentRepository.SaveAsync();
            return true;
        }
        public async Task<Department> GetDeparmentByEmployeeId(int employeeId)
        {
            var department = await _deparmentRepository.GetAllAsync()
            .Include(d => d.Employees)
        .FirstOrDefaultAsync(d => d.Employees.Any(e => e.Id == employeeId));
            return department;


        }
    }
}
