using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Company.Data.Models;
using Company.Service.Dto;

namespace Company.Service.Mapping
{
	public class EmployeeProfile : Profile
	{
		public EmployeeProfile() {
			CreateMap<Employee, EmployeeDto>().ReverseMap();
		}
	}
}
