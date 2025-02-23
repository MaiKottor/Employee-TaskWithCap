using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment.Application.Features.Employee.DTO
{
    public class EmployeeAddedEvent
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
