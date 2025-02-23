using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Domain.Entities
{
   public class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        private readonly List<Employee> _employees = new(); 

        public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();
        public Department( string name)
        {
            Name = name;
        }
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Department name cannot be empty.");

            Name = name;
        }
        public Employee AddEmployee(string name, string email)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Employee name is required.");
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email is required.");

            var employee = new Employee(name, email, this);
            _employees.Add(employee);
            return employee;
        }
        public bool RemoveEmployee(int employeeId)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
                return false;

            _employees.Remove(employee);
            return true;
        }
    }
}
