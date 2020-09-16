using Microsoft.EntityFrameworkCore;

namespace proyecto.Models{


    public class ProyectoContext: DbContext{

        public ProyectoContext(DbContextOptions<ProyectoContext> options): base (options){


        }

        public DbSet<Contacto> Contactos{get;set;}
        public DbSet<User> User{get;set;}

    }
}