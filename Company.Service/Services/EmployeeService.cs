using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
using Company.Service.Helper;
using Company.Service.Interfaces;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
		}

        public void Add(EmployeeDto entityDto)
        {
            //Employee employee = new Employee
            //{
            //    Adress = entity.Adress,
            //    Name = entity.Name,
            //    Age = entity.Age,
            //    CreatedAt = entity.CreatedAt,
            //    Email = entity.Email,
            //    HiringDate = entity.HiringDate,
            //    Salary = entity.Salary,
            //    PhoneNumber = entity.PhoneNumber,
            //    ImageUrl = entity.ImageUrl,
            //    DepartmentId = entity.DepartmentId,
            //};
            entityDto.ImageUrl = DocumentSettings.UploadFile(entityDto.Image, "Images");
            if (string.IsNullOrEmpty(entityDto.ImageUrl))
            {
                throw new Exception("Failed to upload image.");
            }

            Employee employee = _mapper.Map<Employee>(entityDto);
            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();

        }

        public void Delete(EmployeeDto entityDto)
        {
			//Employee employee = new Employee
			//{
			//	Adress = entity.Adress,
			//	Name = entity.Name,
			//	Age = entity.Age,
			//	CreatedAt = entity.CreatedAt,
			//	Email = entity.Email,
			//	HiringDate = entity.HiringDate,
			//	Salary = entity.Salary,
			//	PhoneNumber = entity.PhoneNumber,
			//	ImageUrl = entity.ImageUrl,
			//	DepartmentId = entity.DepartmentId,
			//};
			Employee employee = _mapper.Map<Employee>(entityDto);
			_unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var emp = _unitOfWork.employeeRepository.GetAll();
            //var MappedEmployee = emp.Select(x => new EmployeeDto
            //{
            //    DepartmentId = x.DepartmentId,
            //    Name = x.Name,
            //    Age = x.Age,
            //    CreatedAt = x.CreatedAt,
            //    Email = x.Email,
            //    HiringDate = x.HiringDate,
            //    Salary = x.Salary,
            //    PhoneNumber = x.PhoneNumber,
            //    ImageUrl = x.ImageUrl,
            //    Adress = x.Adress,

            //});
            IEnumerable<EmployeeDto> MappedEmployee = _mapper.Map<IEnumerable<EmployeeDto>>(emp);  
			return MappedEmployee;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            var emp = _unitOfWork.employeeRepository.GetById(id.Value);
            if (emp == null)
                return null;
			//EmployeeDto employeeDto = new EmployeeDto
			//{
			//	Adress = emp.Adress,
			//	Name = emp.Name,
			//	Age = emp.Age,
			//	CreatedAt = emp.CreatedAt,
			//	Email = emp.Email,
			//	HiringDate = emp.HiringDate,
			//	Salary = emp.Salary,
			//	PhoneNumber = emp.PhoneNumber,
			//	ImageUrl = emp.ImageUrl,
			//	DepartmentId = emp.DepartmentId,
			//};
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(emp);
			return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
			IEnumerable<Employee> emp = _unitOfWork.employeeRepository.GetEmployeeByName(name);
			//IEnumerable<EmployeeDto> employeeDto =  new EmployeeDto
			//{
			//	Adress = emp.Adress,
			//    Age = emp.Age,
			//	DepartmentId = emp.DepartmentId,
			//	Email = emp.Email,
			//	HiringDate= emp.HiringDate,
			//	ImageUrl = emp.IngeUrl,
			//	Name = emp.Name,
			//	PhoneNumber = emp.PhoneNumber,
			//	Salary = emp.Salary
			//};
			IEnumerable<EmployeeDto> employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(emp);

			return employeeDto;
		}

        //public void Update(EmployeeDto employee)
        //{
        //    _unitOfWork.employeeRepository.Update(employee);
        //    _unitOfWork.Complete(); 
        //}
    }
}
