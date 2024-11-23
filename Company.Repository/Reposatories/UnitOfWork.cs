using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Contexts;
using Company.Repository.Interfaces;

namespace Company.Repository.Reposatories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public UnitOfWork(CompanyDbContext context)
        {
            departmentRepository = new DepartmentRepository(context);
            employeeRepository = new EmployeeRepository(context);
            _context = context;
        }

        public IDepartmentRepository departmentRepository { get ; set; }
        public IEmployeeRepository employeeRepository { get; set; }
		public int Complete()
		{
			int rowsAffected = _context.SaveChanges();
			Console.WriteLine($"Rows affected: {rowsAffected}");
			return rowsAffected;
		}
	}
}
