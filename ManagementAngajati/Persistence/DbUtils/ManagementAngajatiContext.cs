using ManagementAngajati.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementAngajati.Persistence.DbUtils

{
    public class ManagementAngajatiContext : DbContext
    {
        public ManagementAngajatiContext(DbContextOptions<ManagementAngajatiContext> options) : base(options)
        {
        }


        public DbSet<AngajatEntity> Angajati { get; set; }

        public DbSet<PostEntity> Posturi { get; set; }

        public DbSet<IstoricAngajatEntity> IstoricuriAngajati { get; set; }

        public DbSet<ConcediuEntity> Concedii { get; set; }

        


    }
}
