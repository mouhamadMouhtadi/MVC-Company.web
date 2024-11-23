using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Repository.Reposatories;
using Company.Service.Dto;
using Company.Service.Interfaces;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
	{
        private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DepartmentService(IUnitOfWork unitOfWork , IMapper mapper )
		{
            _unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public void Add(DepartmentDto entityDto)
		{
			var MappedDepartment = new Department()
			{
				Code = entityDto.Code,
				Name = entityDto.Name,
				CreatedAt = DateTime.Now
			};

			Department dept = _mapper.Map<Department>(MappedDepartment);
			_unitOfWork.departmentRepository.Add(dept);
			_unitOfWork.Complete();
		}

        public void Delete(DepartmentDto entity)
		{
			//Department dept = new Department
			//{
			//	Name = entity.Name,
			//	Code = entity.Code,
			//	CreatedAt = DateTime.Now,
			//	Id = entity.Id
			//}
			Department department = _mapper.Map<Department>(entity);
			_unitOfWork.departmentRepository.Delete(department);
		}


        public IEnumerable<DepartmentDto> GetAll()
		{
			var dept = _unitOfWork.departmentRepository.GetAll();
			IEnumerable<DepartmentDto> MappedDepartment = _mapper.Map<IEnumerable<DepartmentDto>>(dept);
			return MappedDepartment;
		}

		public DepartmentDto GetById(int? id)
		{
			if(id is null)
			{
				return null;
			}
			var dept = _unitOfWork.departmentRepository.GetById(id.Value);
			if(dept == null)
				return null;
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(dept);
			return departmentDto;
		}

        DepartmentDto IDepartmentService.GetById(int? id)
        {
            throw new NotImplementedException();
        }

        //public void Update(DepartmentDto Entity)
        //{
        //	var dept = GetById(Entity.Id);
        //	if (dept.Name != Entity.Name)
        //	{
        //		if (GetAll().Any(x=>x.Name == Entity.Name) )
        //		{
        //			throw new Exception("DuplicatedDepartmentName");
        //		}
        //		dept.Name = Entity.Name;
        //		dept.Code = Entity.Code;
        //		_unitOfWork.departmentRepository.Update(dept);
        //		_unitOfWork.Complete();
        //	}
        //}
    }
}
