using System.Reflection.Metadata;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PersonsApp.Dtos.Countries;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;

    namespace PersonsApp.Mappers
    {
        public static class EmployeeMapper
        {
            public static EmployeeEntity CreateDtoToEntity(EmployeeCreateDto dto)
            {
                return new EmployeeEntity
                {
                    EmployeeId = 1,
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Document = dto.Document,
                    HiringDate = dto.HiringDate,
                    Department= dto.Department,
                    PositionJob = dto.PositionJob,
                    BaseSalary = dto.BaseSalary,
                    Activity = dto.Activity

                };
            }
         public static EmployeeEntity EditDtoToEntity (EmployeeEntity entity, EmployeeEditDto dto)
        {
            entity.EmployeeId = dto.EmployeeId;
            entity.Name = dto.Name;
            entity.LastName = dto.LastName;
            entity.Document = dto.Document;
            entity.HiringDate = dto.HiringDate;
            entity.Department = dto.Department;
            entity.PositionJob = dto.PositionJob;
            entity.BaseSalary = dto.BaseSalary;
            entity.Activity = dto.Activity;

            return entity;
        }
        public static List <EmployeeDto> ListEntityToListDto(List<EmployeeEntity> entities) //Devuelve una lista de person DtO y recibe una entidad de personas en formato lista
        {
            var dtos = entities.Select(employee => new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                LastName = employee.LastName,
                Document = employee.Document,
                HiringDate = employee.HiringDate,
                Department = employee.Department,
                PositionJob = employee.PositionJob,
                BaseSalary = employee.BaseSalary
            }).ToList();
            return dtos;
        }
        }
    }
