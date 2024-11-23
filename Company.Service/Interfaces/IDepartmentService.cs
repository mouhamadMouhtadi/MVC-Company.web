using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Models;
using Company.Service.Dto;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
	{

		DepartmentDto GetById(int? id);
		IEnumerable<DepartmentDto> GetAll();
		void Add(DepartmentDto department);
		//void Update(DepartmentDto department);
		void Delete(DepartmentDto department);
	}
}
