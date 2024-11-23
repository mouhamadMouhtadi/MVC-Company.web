using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Models;

namespace Company.Service.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; } = new HashSet<EmployeeDto>();
        public bool  IsDeleted { get; set; }
    }
}
