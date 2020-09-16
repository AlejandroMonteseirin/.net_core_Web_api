using System;

namespace proyecto.Models
{
    public class Contacto{

        //si tiene la interrogacion quiere decir que permite valores nulos
        public long? id {get;set;}
        public string nombre{get;set;}
        public string email {get;set;}
        public DateTime? nace{get;set;}
        public string mensaje{get;set;}

        public string apellidos{get;set;}

    }


}



