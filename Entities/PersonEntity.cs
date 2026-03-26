using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsApp.Entities
{
    [Table("persons")] //USADO PARA FORZAR EL NOMBRE QUE QUEREMOS
    public class PersonEntity : BaseEntity //My first API 
    {   
        [Required()]
        [StringLength(13)]
        [Column("dni")]
        public string  DNI { get; set; } //Se almacena como cadena de texto para que en la base de datos no omita valores.
        //Validacion Nombres

        [Required()]
        [StringLength(40)]
        [Column("first_name")]

        public string  FirstName { get; set; }
        //Validacion Apellidos
        [Required()]
        [StringLength(50)]
        [Column("last_name")]
        public string  LastName { get; set; }
        // [StringLength(11, ErrorMessage ="La fecha debe contener de 8-10 Caracteres.",MinimumLength = 8)]
        [Column("birth_date")]
        public DateTime  BirthDate { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Required]
        [Column("country_id")]
        public string CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual CountryEntity Country { get; set; }
    }
}