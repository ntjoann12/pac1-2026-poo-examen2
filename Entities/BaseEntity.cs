using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsApp.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]  
        public string Id { get; set; }
        //Audit fields (Sirve para tener registro de los movimientos en una base de datos)
        [Column("created_by_id")]
        public string CreatedById { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("updated_by_id")]
        public string UpdatedById { get; set; }
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
        
    }
}