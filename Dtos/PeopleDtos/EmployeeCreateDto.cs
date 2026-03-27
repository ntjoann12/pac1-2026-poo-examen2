
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersonsApp.Dtos.Persons
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = "El ID es requerido.")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El apellido es requerido.")]
        public string  LastName { get; set; }
        [Required]
        [Unicode]
        public string Document { get; set; }
        public DateTime HiringDate { get; set; }
        public string Department { get; set; }
        public string PositionJob { get; set; }
        public decimal BaseSalary { get; set; }
        public bool Activity { get; set; }
        
    }
}