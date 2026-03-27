using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PersonsApp.Constants;
using PersonsApp.DataBase;
using PersonsApp.Dtos.Common;
using PersonsApp.Dtos.Countries;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;
using PersonsApp.Mappers;

namespace PersonsApp.Services.Employees
{
    
    public class EmployeeService : IEmployeeService //Clase : Interfaz (contrato que define las reglas de como se va a comportar PersonService)
    {
        private readonly AppDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public EmployeeService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize"); //Hace conversion a entero
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit"); //Hace conversion a entero

        }
        public async Task<ResponseDto<PageDto<List<EmployeeDto>>>> GetAllEmployeesAsync(string searchTerm = "", int  page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE  : pageSize;//Si no viene nada en "pageSize" pone el valor por defecto, sino, agarra el valor asignado en pageSize
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT  : pageSize;//Si no viene nada en "pageSize" pone el valor por defecto, sino, agarra el valor asignado en pageSize

            int startIndex = (page - 1 ) * pageSize; // define pagina de inicio

            IQueryable<EmployeeEntity> employeesQuery = _context.Employees.Include(e => e.EmployeeId );

            if(!string.IsNullOrEmpty(searchTerm))
            {
                employeesQuery = employeesQuery.Where( x => (x.EmployeeId + " " + x.Name + " " + x.LastName).Contains(searchTerm) ); //() es para concatenear
            }
            int totalRows = await employeesQuery .CountAsync(); //Muestra el total de registros
            var employeeEntity = await employeesQuery 
                .OrderBy(x => x.Name)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();
            
            var employeeDto = EmployeeMapper.ListEntityToListDto(employeeEntity);

            // respuesta
            return new ResponseDto<PageDto<List<EmployeeDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<EmployeeDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows, 
                    TotalPages = (int)Math.Ceiling((double)totalRows/pageSize), //Cuando se pone entre parentesis, el tipo de dato que este en medio, se va a convertir al dato que escojamos
                    Items = EmployeeMapper.ListEntityToListDto(employeeEntity),
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && 
                    page < (int)Math.Ceiling((double)totalRows/pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }
    public async Task<ResponseDto<EmployeeDto>> GetOneEmployeeByIdAsync (int id)
        {
            var employeeEntity = await _context.Employees.FirstOrDefaultAsync(p => p.EmployeeId == id );
           
            if(employeeEntity is null)
            {
                return new ResponseDto<EmployeeDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false
                };
            }

            return new ResponseDto<EmployeeDto>
            {
                 StatusCode = HttpStatusCode.OK,
                    Message = HttpMessageResponse.REGISTER_FOUND,
                    Status = true,
                    Data = new EmployeeDto
                    {
                        EmployeeId = employeeEntity.EmployeeId,
                        Name = employeeEntity.Name,
                        LastName = employeeEntity.LastName,
                        Document = employeeEntity.Document,
                        HiringDate = employeeEntity.HiringDate,
                        Department = employeeEntity.Department,
                        PositionJob = employeeEntity.PositionJob,
                        BaseSalary = employeeEntity.BaseSalary
                    }
            };
        }
    
    public async Task<ResponseDto<PageDto<List<EmployeeDto>>>> GetActiveEmployeesAsync (bool searchTerm = true, int  page = 1, int pageSize = 10)
        {
            
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE  : pageSize;//Si no viene nada en "pageSize" pone el valor por defecto, sino, agarra el valor asignado en pageSize
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT  : pageSize;//Si no viene nada en "pageSize" pone el valor por defecto, sino, agarra el valor asignado en pageSize

            int startIndex = (page - 1 ) * pageSize; // define pagina de inicio

            IQueryable<EmployeeEntity> employeesQuery = _context.Employees.Include(e => e.EmployeeId );


            int totalRows = await employeesQuery.CountAsync(); //Muestra el total de registros
            var employeeEntity = await employeesQuery 
                .OrderBy(x => x.Name)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();
            
            var employeeDto = EmployeeMapper.ListEntityToListDto(employeeEntity);
            
            if(searchTerm)
            {
                employeesQuery = employeesQuery.Where(e => e.Activity == true);
                 return new ResponseDto<PageDto<List<EmployeeDto>>>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = true,
                        Message = HttpMessageResponse.REGISTERS_FOUND,
                        Data = new PageDto<List<EmployeeDto>>
                        {
                            CurrentPage = page,
                            PageSize = pageSize,
                            TotalItems = totalRows, 
                            TotalPages = (int)Math.Ceiling((double)totalRows/pageSize), //Cuando se pone entre parentesis, el tipo de dato que este en medio, se va a convertir al dato que escojamos
                            Items = EmployeeMapper.ListEntityToListDto(employeeEntity),
                            HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && 
                            page < (int)Math.Ceiling((double)totalRows/pageSize),
                            HasPreviousPage = page > 1
                        }
                    };
            }

            return null;
            
            
        }

    public async Task<ResponseDto<EmployeeActionResponseDto>> CreateEmployeeAsync(EmployeeCreateDto dto) 

    {
        EmployeeEntity employeeEntity = EmployeeMapper.CreateDtoToEntity(dto);
        _context.Employees.Add(employeeEntity);
       
        await _context.SaveChangesAsync();
            return new ResponseDto<EmployeeActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = new EmployeeActionResponseDto
                    {
                        Activity = true
                    }
            };
    }



    public async Task<ResponseDto<EmployeeActionResponseDto>> EditEmployeeAsync(int employeeid, EmployeeEditDto dto)
        {
            var employeeEntity = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeid);

                if (employeeEntity is null)
                {
                    return new ResponseDto<EmployeeActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.NOT_FOUND,
                        Status = false,
                        Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                        Data = new EmployeeActionResponseDto
                            {
                                Activity = false
                            }
                    };
                }

                EmployeeMapper.EditDtoToEntity(employeeEntity, dto);

                await _context.SaveChangesAsync();

                return new ResponseDto<EmployeeActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = HttpMessageResponse.REGISTER_UPDATED,
                    Data = new EmployeeActionResponseDto
                        {
                            Activity = true
                        }
                };
        }
       
       public async Task<ResponseDto<EmployeeActionResponseDto>> DeleteEmployeeAsync (int employee_id)
        {
           var employeeEntity = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee_id);

            if (employeeEntity is null)
            {
                return new ResponseDto<EmployeeActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Data = new EmployeeActionResponseDto
                    {
                        Activity = true
                    }
                };
            }

            return new ResponseDto<EmployeeActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_SWITCH_STATE,
                Data = new EmployeeActionResponseDto
                {
                    Activity = false 
                }
            };
         }
     }
}
