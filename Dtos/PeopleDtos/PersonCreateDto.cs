
using System.ComponentModel.DataAnnotations;

namespace PersonsApp.Dtos.Persons
{
    public class PersonCreateDto
    {
        [Required(ErrorMessage = "El DNI es requerido.")]
        [StringLength(13, ErrorMessage = "El DNI debe tener 13 digitos.", MinimumLength = 13)] //Agregar validacion de nombres y apellidos.
        public string  DNI { get; set; } //Se almacena como cadena de texto para que en la base de datos no omita valores.
        //Validacion Nombres

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(50 ,ErrorMessage ="Los {0} deben tener un minimo {2} y maximo de {1} caracteres.",MinimumLength = 3 )]
        public string  FirstName { get; set; }
        //Validacion Apellidos
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(50 ,ErrorMessage ="Los {0} deben tener un minimo {2} y maximo de {1} caracteres.",MinimumLength = 3 )]
        public string  LastName { get; set; }
        // [StringLength(11, ErrorMessage ="La fecha debe contener de 8-10 Caracteres.",MinimumLength = 8)]
        public DateTime  BirthDate { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El {0} es requerido")]

        public string CountryId { get; set; }

        //TODO: Agregar validacion de GUID. 
    }
}