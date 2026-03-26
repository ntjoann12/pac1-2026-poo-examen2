
using Microsoft.EntityFrameworkCore;
using PersonsApp.Entities;

namespace PersonsApp.DataBase
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options) //Base hace referencia al metodo constructor
        {
            
        }
        public DbSet<PersonEntity> Persons { get; set; } //Base t espera cualquier cosa, en este caso una entidad
        public DbSet<CountryEntity> Countries { get; set; } //Base t espera cualquier cosa, en este caso una entidad
        
    }
}