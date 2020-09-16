using System;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Models
{
    public class User{

        [Key] //es la primary key
        [Required] // es obligatorio
        [Display(Name="Username")]
        [StringLength(20,ErrorMessage="el valor para {0} debe contener al menos {2} y máximo {1} caracteres",MinimumLength=6)]
        public string username {get;set;}

        public byte[] PasswordHash{get; set;}
        public byte[] PasswordSalt{get; set;}

        public string email {get; set;}

        [DataType(DataType.Date)]
        public DateTime FechaCreado {get; set;}
       

    }


}



