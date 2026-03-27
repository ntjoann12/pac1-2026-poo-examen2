using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsApp.Entities
{
    [Table("worksheet_detail")]
    public class SpreadSheetDetailEntity : BaseEntity
    {
        [Key]
        [Column("worksheetdetail_id")] 
        public int WorkSheetDetailId { get; set; }

        [ForeignKey(nameof(WorkSheetId))]
        [Column("worksheet_id")]  
        public int WorkSheetId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [Column("employee_id")]  
        public int EmployeeId { get; set; }
        [Column("base_salary")]  
        public decimal BaseSalary { get; set; }
        [Column("extra_hours")]  
        public decimal ExtraHours { get; set; }
        [Column("mount_extra_hours")]  
        public decimal MountExtraHours { get; set; }
        [Column("bonuses")]  
        public decimal Bonuses { get; set; }
        [Column("deductions")]  
        public decimal Deductions { get; set; }
        [Column("net_salary")]  
        public decimal NetSalary{ get; set; }
        [Column("comments")]  
        public string Comments { get; set; }


    }
}