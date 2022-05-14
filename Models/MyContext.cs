using Microsoft.EntityFrameworkCore;

namespace MeToMe.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        
        public DbSet<Movie> Movies {get;set;}
        public DbSet<Actor> Actors {get;set;}
        public DbSet<Cast> Cast {get;set;}
    }
}
