using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;

namespace Company.Repository.Reposatories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public void Add(Employee employee)
            => _context.Add(employee);

        public void Delete(Employee employee)
            =>_context.Remove(employee);

        public IEnumerable<Employee> GetAll()
            => _context.Employees.ToList();

        public Employee GetById(int id)
            => _context.Employees.Find(id);

        public IEnumerable<Employee> GetEmployeeByAdress(int adress)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        =>  _context.Employees.Where(x => 
        x.Name.Trim().ToLower().Contains(name.Trim().ToLower()) ||
        x.PhoneNumber.Trim().ToLower().Contains(name.Trim().ToLower()) ||
        x.Email.Trim().ToLower().Contains(name.Trim().ToLower())
        ).ToList();
       

        public void Update(Employee employee)
            =>_context.Update(employee);
    }
}
