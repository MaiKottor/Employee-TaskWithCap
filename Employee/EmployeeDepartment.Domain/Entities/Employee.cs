using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Domain.Entities
{
   public class Employee
    {
        public int Id { get; private set; } 
        public string Name { get; private set; }
        public string Email { get; private set; }
        public int DepartmentId { get; private set; }
        public Department Department { get; private set; }
        private Employee() { } 

        internal Employee(string name, string email, Department department)
        {
            Name = name;
            Email = email;
            Department = department ?? throw new ArgumentNullException(nameof(department));
            DepartmentId = department.Id;
        }
        public void UpdateDetails(string name, string email,int departmentId)
        {
            Name = name;
            Email = email;
            DepartmentId = departmentId;
        }


    }
}
