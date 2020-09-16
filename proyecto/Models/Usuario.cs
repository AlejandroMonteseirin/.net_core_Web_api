using System;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class Usuario{

        [Key] //es la primary key
        [Required] // es obligatorio
        [Display(Name="Username")]
        [StringLength(20,ErrorMessage="el valor para {0} debe contener al menos {2} y máximo {1} caracteres",MinimumLength=6)]
        public string username {get;set;}


        [Required] // es obligatorio
        [StringLength(100,ErrorMessage="el valor para {0} debe contener al menos {2} y máximo {1} caracteres",MinimumLength=6)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string password {get;set;}



        public string email {get; set;}

        [DataType(DataType.Date)]
        public DateTime FechaCreado {get; set;}
       

    }


}



