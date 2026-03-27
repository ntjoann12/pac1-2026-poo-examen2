using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsApp.Entities
{
    [Table("worksheet")]
    public class SpreadSheetEntity :BaseEntity
    {
         [Key]
        [Column("worksheet_id")]  
        public int WorkSheetId { get; set; }
        [Required]
        [Column("period")]
        public string Period { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("payment_date")]
        public DateTime PaymentDate { get; set; }
        [Column("state")]
        public string Estado { get; set; }
    }
}