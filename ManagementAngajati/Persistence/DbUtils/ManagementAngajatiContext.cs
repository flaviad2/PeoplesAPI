using ManagementAngajati.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementAngajati.Persistence.DbUtils

{
    public class ManagementAngajatiContext : DbContext
    {
        public ManagementAngajatiContext(DbContextOptions<ManagementAngajatiContext> options) : base(options)
        {
        }



        public DbSet<Angajat> Angajati { get; set; }

        public DbSet<Post> Posturi { get; set; }

        public DbSet<IstoricAngajat> IstoricuriAngajati { get; set; }

        public DbSet<Concediu> Concedii { get; set; }

        


    }
}
