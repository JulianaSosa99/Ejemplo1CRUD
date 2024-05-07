using Ejemplo1.Models;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo1.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        //Agregar los modelos aqui (cada modelo corresponde a una tabla en la base de datos)
       public DbSet<Contacto> Contacto {  get; set; }
        
    }
}
