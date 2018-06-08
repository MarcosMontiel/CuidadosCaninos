using Microsoft.EntityFrameworkCore;

namespace Cuidados.Caninos.Marcos.Montiel.Models
{
    public class CCContext : DbContext
    {
        public CCContext(DbContextOptions<CCContext> options)             : base(options)         {
        }          public DbSet<ComCatEscolaridad> ComCatEscolaridad { get; set; }         public DbSet<ComCatSexo> ComCatSexo { get; set; }         public DbSet<ComPersona> ComPersona { get; set; }
    }
}