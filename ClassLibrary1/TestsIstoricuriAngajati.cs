using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ManagementAngajati.Persistence.Entities;
using ManagementAngajati.Persistence;
using ManagementAngajati.Models;

namespace TestsManagementAngajati
{
    public class TestsIstoricuriAngajati
    {
        private DbContextOptions<ManagementAngajatiContext>? options;

        public TestsIstoricuriAngajati()
        {

        }
        
        public void SampleDataDb(DbContextOptions<ManagementAngajatiContext>? options)
        {
            using (var context = new ManagementAngajatiContext(options))
            {

                List<PostEntity> posturi_initial = new List<PostEntity>();
                List<AngajatEntity> angajati_initial = new List<AngajatEntity>();

                PostEntity p1 = new PostEntity(1, "Tester", "Detalii functie", "Digital", angajati_initial);
                PostEntity p2 = new PostEntity(2, "Scrum master", "Detalii functie detalii", "Digital", angajati_initial);
                PostEntity p3 = new PostEntity(3, "Programator", "Alte detalii", "Digital", angajati_initial);

                AngajatEntity a1 = new AngajatEntity(1, "Pop", "Mihai", "mita", "parola1", DateTime.Now, "M", 2, posturi_initial);
                AngajatEntity a2 = new AngajatEntity(2, "Codreanu", "Rares", "rares", "parola2", DateTime.Now, "M", 3, posturi_initial);
                AngajatEntity a3 = new AngajatEntity(3, "Dorobat", "Flavia", "flavi", "parola3", DateTime.Now, "F", 0, posturi_initial);

                //primul angajat poate lucra pe posturile p2 si p3
                List<PostEntity> posturi_first = new List<PostEntity>();
                posturi_first.Add(p2);
                posturi_first.Add(p3);

                //pe primul post avem angajatii a2 si a3
                List<AngajatEntity> angajati_first = new List<AngajatEntity>();
                angajati_first.Add(a2);
                angajati_first.Add(a3);


                List<PostEntity> posturi_second = new List<PostEntity>();
                posturi_second.Add(p1);

                List<AngajatEntity> angajati_second = new List<AngajatEntity>();
                angajati_second.Add(a1);

                List<PostEntity> posturi_third = new List<PostEntity>();
                posturi_third.Add(p1);
                posturi_third.Add(p2);
                posturi_first.Add(p3);

                List<AngajatEntity> angajati_third = new List<AngajatEntity>();
                angajati_third.Add(a1);
                angajati_third.Add(a2);
                angajati_third.Add(a3);

                a1.IdPosturi = posturi_first;
                a2.IdPosturi = posturi_second;
                a3.IdPosturi = posturi_third;

                p1.Angajati = angajati_first;
                p2.Angajati = angajati_second;
                p3.Angajati = angajati_third;


                context.Angajati.Add(a1);
                context.Angajati.Add(a2);
                context.Angajati.Add(a3);

                context.Posturi.Add(p1);
                context.Posturi.Add(p2);
                context.Posturi.Add(p3);

              

                IstoricAngajatEntity i1 = new IstoricAngajatEntity(1, a1, p1, new DateTime(2021, 7, 10, 7, 10, 24), 3000, new DateTime(2023, 7, 10, 7, 10, 24));
                IstoricAngajatEntity i2 = new IstoricAngajatEntity(2, a2, p2, new DateTime(2019, 8, 10, 7, 10, 24), 3500, new DateTime(2023, 7, 10, 7, 10, 24));
                IstoricAngajatEntity i3 = new IstoricAngajatEntity(3, a3, p3, new DateTime(2021, 9, 10, 7, 10, 24), 2500, null);
                IstoricAngajatEntity i4 = new IstoricAngajatEntity(4, a3, p1, new DateTime(2021, 9, 10, 7, 10, 24), 2500, null);
                IstoricAngajatEntity i5 = new IstoricAngajatEntity(5, a3, p2, new DateTime(2021, 9, 10, 7, 10, 24), 2500, null);

                context.IstoricuriAngajati.Add(i1);
                context.IstoricuriAngajati.Add(i2);
                context.IstoricuriAngajati.Add(i3);
                context.IstoricuriAngajati.Add(i4);
                context.IstoricuriAngajati.Add(i5); 


                context.SaveChanges();

            }
        }

        [Fact]
        public void GetAllIstoricuriTest()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                 .UseInMemoryDatabase(databaseName: "IstoricuriAngajati1")
                 .Options;

            SampleDataDb(options);

            using (var context = new ManagementAngajatiContext(options))
            {

                RepositoryIstoricAngajat repoIstoricAngajati = new RepositoryIstoricAngajat(context);
                List<IstoricAngajat> istoricuriAngajat = repoIstoricAngajati.FindAll().Result;

                Assert.Equal(5, istoricuriAngajat.Count);

                Assert.Equal(1, istoricuriAngajat[0].ID);
                Assert.Equal(2, istoricuriAngajat[1].ID);
                Assert.Equal(3, istoricuriAngajat[2].ID);
                Assert.Equal(4, istoricuriAngajat[3].ID);
                Assert.Equal(5, istoricuriAngajat[4].ID);

                Assert.Equal(3000, istoricuriAngajat[0].Salariu);
                Assert.Equal(3500, istoricuriAngajat[1].Salariu);
                Assert.Equal(2500, istoricuriAngajat[2].Salariu);
                Assert.Equal(2500, istoricuriAngajat[3].Salariu);
                Assert.Equal(2500, istoricuriAngajat[4].Salariu);

                Assert.Equal(new DateTime(2023, 7, 10, 7, 10, 24), istoricuriAngajat[0].DataReziliere);
                Assert.Equal(new DateTime(2023, 7, 10, 7, 10, 24), istoricuriAngajat[1].DataReziliere);
                Assert.Equal(null, istoricuriAngajat[2].DataReziliere);
                Assert.Equal(null, istoricuriAngajat[3].DataReziliere);
                Assert.Equal(null, istoricuriAngajat[4].DataReziliere); 
                
            }

        }

        [Fact]
        public async Task AddIstoricAngajat_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                   .UseInMemoryDatabase(databaseName: "IstoricAngajatiDbb2")
                   .Options;

            SampleDataDb(options); 

            using(var context = new ManagementAngajatiContext(options))
            {
                RepositoryIstoricAngajat repositoryIstoricAngajat = new RepositoryIstoricAngajat(context);

                RepositoryAngajat repositoryAngajat = new RepositoryAngajat(context);

                Angajat angajat = repositoryAngajat.FindOne(1).Result;

                IstoricAngajat istoricAngajat = await repositoryIstoricAngajat.Add(new IstoricAngajat { IdAngajat = angajat.ID, DataAngajare = new DateTime(2021, 7, 10, 7, 10, 24), DataReziliere = new DateTime(2021, 7, 10, 7, 10, 24) });

                List<IstoricAngajat> istoricuriAngajat = repositoryIstoricAngajat.FindAll().Result;

                Assert.Equal(6, istoricuriAngajat.Count); 
                Assert.Equal(6, istoricuriAngajat[5].ID);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), istoricuriAngajat[5].DataAngajare);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), istoricuriAngajat[5].DataReziliere); 

            }

        }

        [Fact]
        public async Task DeletePost_ValidCall()
        {

            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                 .UseInMemoryDatabase(databaseName: "IstoricuriAngajatiDb3")
                 .Options;

            SampleDataDb(options); 

            using(var context = new ManagementAngajatiContext(options))
            {
                RepositoryIstoricAngajat repositoryIstoricAngajat = new RepositoryIstoricAngajat(context);

                repositoryIstoricAngajat.Delete(1);
                repositoryIstoricAngajat.Delete(2);

                List<IstoricAngajat> istoricuriAngajat = repositoryIstoricAngajat.FindAll().Result;

                Assert.Equal(3, istoricuriAngajat.Count);

                Assert.Equal(3, istoricuriAngajat[0].ID);
                Assert.Equal(4, istoricuriAngajat[1].ID);
                Assert.Equal(5, istoricuriAngajat[2].ID);
               


            }


        }

        [Fact]
        public async Task UpdateIstoric_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                              .UseInMemoryDatabase(databaseName: "IstoricuriAngajatiDb6")
                              .Options;

            SampleDataDb(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryIstoricAngajat repositoryIstoricAngajat = new RepositoryIstoricAngajat(context); 

                RepositoryAngajat repositoryAngajat = new RepositoryAngajat(context);

                Angajat angajat = repositoryAngajat.FindOne(1).Result;

                IstoricAngajat  newObj = new IstoricAngajat { ID=1, IdAngajat = angajat.ID, DataAngajare = new DateTime(2010, 7, 10, 7, 10, 24), DataReziliere = new DateTime(2030, 7, 10, 7, 10, 24) };

                IstoricAngajat istoricModif = repositoryIstoricAngajat.Update(newObj, 1).Result;

                Assert.Equal(1, istoricModif.ID);
                Assert.Equal(new DateTime(2010, 7, 10, 7, 10, 24), istoricModif.DataAngajare);
                Assert.Equal(new DateTime(2030, 7, 10, 7, 10, 24), istoricModif.DataReziliere);

                istoricModif = repositoryIstoricAngajat.FindOne(1).Result;

                Assert.Equal(1, istoricModif.ID);
                Assert.Equal(new DateTime(2010, 7, 10, 7, 10, 24), istoricModif.DataAngajare);
                Assert.Equal(new DateTime(2030, 7, 10, 7, 10, 24), istoricModif.DataReziliere);




            }


        }  
    }
}
