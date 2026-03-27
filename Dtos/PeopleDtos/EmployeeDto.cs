
using PersonsApp.Dtos.Countries;

namespace PersonsApp.Dtos.Persons
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string  LastName { get; set; }
        public string Document { get; set; }
        public DateTime HiringDate { get; set; }
        public string Department { get; set; }
        public string PositionJob { get; set; }
        public decimal BaseSalary { get; set; }
    }
}