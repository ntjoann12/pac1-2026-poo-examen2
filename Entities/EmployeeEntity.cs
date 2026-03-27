using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonsApp.Entities
{
    [Table("employees")]
    public class EmployeeEntity : BaseEntity
    {  
        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("last_name")]
        public string  LastName { get; set; }
        [Required]
        [Unicode]
        [Column("document")]
        public string Document { get; set; }
        [Column("hiring_date")]
        public DateTime HiringDate { get; set; }
        [Column("department")]
        public string Department { get; set; }
        [Column("positions_job")]
        public string PositionJob { get; set; }
        [Column("base_salary")]
        public decimal BaseSalary { get; set; }
        [Column("activity")]
        public bool Activity { get; set; }
    }
}