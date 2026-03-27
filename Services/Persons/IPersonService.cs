using PersonsApp.Dtos.Common;
using PersonsApp.Dtos.Persons;

namespace PersonsApp.Services.Employees
{
    public interface IEmployeeService
    {
        Task<ResponseDto<PageDto<List<EmployeeDto>>>> GetAllEmployeesAsync(string searchTerm = "", int  page = 1, int pageSize = 10);
        Task<ResponseDto<EmployeeDto>> GetOneEmployeeByIdAsync (int id);
        Task<ResponseDto<PageDto<List<EmployeeDto>>>> GetActiveEmployeesAsync (bool searchTerm = true, int  page = 1, int pageSize = 10);
        Task<ResponseDto<EmployeeActionResponseDto>> CreateEmployeeAsync(EmployeeCreateDto dto);
        Task<ResponseDto<EmployeeActionResponseDto>> EditEmployeeAsync(int employeeid, EmployeeEditDto dto);

        Task<ResponseDto<EmployeeActionResponseDto>> DeleteEmployeeAsync (int employee_id);


    }
}