using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Models;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>   
    {
        public IEnumerable<Employee> GetEmployeeByName (string name);
        public IEnumerable<Employee> GetEmployeeByAdress (int adress);
    }
}
