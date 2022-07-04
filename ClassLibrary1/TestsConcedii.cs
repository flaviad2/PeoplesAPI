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
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace TestsManagementAngajati
{
    public class TestsConcedii
    {
        private DbContextOptions<ManagementAngajatiContext>? options;

        public TestsConcedii()
        {

        }

        public void SampleDataDb(DbContextOptions<ManagementAngajatiContext>? options)
        {
            using (var context = new ManagementAngajatiContext(options))
            {

                List<Post> posturi_initial = new List<Post>();
                List<Angajat> angajati_initial = new List<Angajat>();

                Post p1 = new Post(1, "Tester", "Detalii functie", "Digital", angajati_initial);
                Post p2 = new Post(2, "Scrum master", "Detalii functie detalii", "Digital", angajati_initial);
                Post p3 = new Post(3, "Programator", "Alte detalii", "Digital", angajati_initial);

                Angajat a1 = new Angajat(1, "Pop", "Mihai", "mita", "parola1", DateTime.Now, "M", 2, posturi_initial);
                Angajat a2 = new Angajat(2, "Codreanu", "Rares", "rares", "parola2", DateTime.Now, "M", 3, posturi_initial);
                Angajat a3 = new Angajat(3, "Dorobat", "Flavia", "flavi", "parola3", DateTime.Now, "F", 0, posturi_initial);

                //primul angajat poate lucra pe posturile p2 si p3
                List<Post> posturi_first = new List<Post>();
                posturi_first.Add(p2);
                posturi_first.Add(p3);

                //pe primul post avem angajatii a2 si a3
                List<Angajat> angajati_first = new List<Angajat>();
                angajati_first.Add(a2);
                angajati_first.Add(a3);


                List<Post> posturi_second = new List<Post>();
                posturi_second.Add(p1);

                List<Angajat> angajati_second = new List<Angajat>();
                angajati_second.Add(a1);

                List<Post> posturi_third = new List<Post>();
                posturi_third.Add(p1);
                posturi_third.Add(p2);
                posturi_first.Add(p3);

                List<Angajat> angajati_third = new List<Angajat>();
                angajati_third.Add(a1);
                angajati_third.Add(a2);
                angajati_third.Add(a3);

                a1.Posturi = posturi_first;
                a2.Posturi = posturi_second;
                a3.Posturi = posturi_third;

                p1.Angajati = angajati_first;
                p2.Angajati = angajati_second;
                p3.Angajati = angajati_third;


                context.Angajati.Add(a1);
                context.Angajati.Add(a2);
                context.Angajati.Add(a3);

                context.Posturi.Add(p1);
                context.Posturi.Add(p2);
                context.Posturi.Add(p3);


                Concediu c1 = new Concediu(1, a1, new DateTime(2021, 7, 10, 7, 10, 24) , new DateTime(2021, 7, 10, 7, 10, 24).AddDays(7));
                Concediu c2 = new Concediu(2, a2, new DateTime(2021, 7, 10, 7, 10, 24), new DateTime(2021, 7, 10, 7, 10, 24).AddDays(10));
                Concediu c3 = new Concediu(3, a3, new DateTime(2021, 7, 10, 7, 10, 24), new DateTime(2021, 7, 10, 7, 10, 24).AddDays(14));

                c1.Angajat = a1;
                c2.Angajat = a2;
                c3.Angajat = a3;

               


                context.Concedii.Add(c1);
                context.Concedii.Add(c2);
                context.Concedii.Add(c3);

                context.SaveChanges();

            }
        }

        [Fact]
        public async void GetAllConcediiTest()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
             .UseInMemoryDatabase(databaseName: "ConcediiDB11")
             .Options;

            SampleDataDb(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repositoryConcediu = new RepositoryConcediu(context);
                List<Concediu> concedii = await repositoryConcediu.FindAll();

                Assert.Equal(3, concedii.Count);

                Assert.Equal(1, concedii[0].ID);
                Assert.Equal(2, concedii[1].ID);
                Assert.Equal(3, concedii[2].ID);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[0].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(7), concedii[0].DataTerminare);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[1].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(10), concedii[1].DataTerminare);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[2].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(14), concedii[2].DataTerminare);

               
              

            }
        }


        [Fact]
        public async Task AddConcediu_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                   .UseInMemoryDatabase(databaseName: "ConcediiDb2")
                   .Options;

            SampleDataDb(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repoConcediu = new RepositoryConcediu(context);
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                Angajat angajat = await repoAngajat.Add(new Angajat { Username = "silvi", Nume = "Pirlea", Password = "parola4", Prenume = "Silvia", DataNasterii = DateTime.Now, Experienta = 2, Posturi = new List<Post>(), Sex = "F" });

                //pune id automat
                Concediu concediu = await repoConcediu.Add(new Concediu { Angajat = angajat, DataIncepere = new DateTime(2021, 7, 10, 7, 10, 24), DataTerminare = new DateTime(2021, 7, 10, 7, 10, 24).AddDays(20) });

                List<Concediu> concedii = repoConcediu.FindAll().Result;

                Assert.Equal(4, concedii.Count);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concediu.DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(20), concediu.DataTerminare);




            }
        }

            [Fact]
            public async Task AddConcediu_InvalidCall()
            {

            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                      .UseInMemoryDatabase(databaseName: "ConcediiDb3")
                      .Options;

            SampleDataDb(options); 

            using(var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repoConcediu = new RepositoryConcediu(context);
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                Angajat angajat = await repoAngajat.Add(new Angajat { Username = "silvi", Nume = "Pirlea", Password = "parola4", Prenume = "Silvia", DataNasterii = DateTime.Now, Experienta = 2, Posturi = new List<Post>(), Sex = "F" });

                //id se repeta
                try
                {
                    Concediu concediu = await repoConcediu.Add(new Concediu { ID = 1, Angajat = angajat, DataIncepere = new DateTime(2021, 7, 10, 7, 10, 24), DataTerminare = new DateTime(2021, 7, 10, 7, 10, 24).AddDays(20) });
                    Assert.True(false);
                }
                catch(Exception e)
                {
                    Assert.True(true); 
                }
                List<Concediu> concedii = repoConcediu.FindAll().Result;

                Assert.Equal(3, concedii.Count);

                

            }
        }

        [Fact]
        public async Task DeleteConcediu_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                .UseInMemoryDatabase(databaseName: "ConcediiDb5")
                .Options;

            SampleDataDb(options); 

            using(var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repositoryConcediu = new RepositoryConcediu(context);
                repositoryConcediu.Delete(1);
                repositoryConcediu.Delete(2);

                List<Concediu> concedii = repositoryConcediu.FindAll().Result;

                Assert.Equal(1, concedii.Count);

                
                Assert.Equal(3, concedii[0].ID);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[0].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(14), concedii[0].DataTerminare);


            }
        }

        [Fact]
        public async Task DeleteConcediu_NoEffect()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                .UseInMemoryDatabase(databaseName: "ConcediiDb6")
                .Options;

            SampleDataDb(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repositoryConcediu = new RepositoryConcediu(context);

                repositoryConcediu.Delete(5);
                repositoryConcediu.Delete(6);

                List<Concediu> concedii = repositoryConcediu.FindAll().Result;

                Assert.Equal(3, concedii.Count);

                Assert.Equal(1, concedii[0].ID);
                Assert.Equal(2, concedii[1].ID);
                Assert.Equal(3, concedii[2].ID);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[0].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(7), concedii[0].DataTerminare);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[1].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(10), concedii[1].DataTerminare);

                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24), concedii[2].DataIncepere);
                Assert.Equal(new DateTime(2021, 7, 10, 7, 10, 24).AddDays(14), concedii[2].DataTerminare);



            }
        }

        [Fact]
        public async Task UpdateConcediu_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                 .UseInMemoryDatabase(databaseName: "ConcediiDB8")
                 .Options;

            SampleDataDb(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repositoryConcediu = new RepositoryConcediu(context);

                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                Angajat angajat = await repoAngajat.FindOne(1); 

                Concediu newObj = new Concediu { ID=1, Angajat=angajat, DataIncepere=new DateTime(2030, 7, 10, 7, 10, 24), DataTerminare= new DateTime(2030, 7, 10, 7, 10, 24).AddDays(30) };

                Concediu concediuModif = repositoryConcediu.Update(newObj, 1).Result;

                Assert.Equal(1, concediuModif.ID);
                Assert.Equal(new DateTime(2030, 7, 10, 7, 10, 24), concediuModif.DataIncepere);
                Assert.Equal(new DateTime(2030, 7, 10, 7, 10, 24).AddDays(30), concediuModif.DataTerminare);

                Concediu cModifCautat = repositoryConcediu.FindOne(1).Result;

                Assert.Equal(1, cModifCautat.ID);
                Assert.Equal(new DateTime(2030, 7, 10, 7, 10, 24), cModifCautat.DataIncepere);
                Assert.Equal(new DateTime(2030, 7, 10, 7, 10, 24).AddDays(30), cModifCautat.DataTerminare);



            }
        }

        [Fact]
        public async Task UpdateConcediu_NoEffectCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "ConcediiDb9")
                    .Options;

            SampleDataDb(options); 

            using(var context = new ManagementAngajatiContext(options))
            {
                RepositoryConcediu repositoryConcediu = new RepositoryConcediu(context);

                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                Angajat angajat = await repoAngajat.FindOne(1);

                Concediu newObj = new Concediu { ID = 9, Angajat = angajat, DataIncepere = new DateTime(2030, 7, 10, 7, 10, 24), DataTerminare = new DateTime(2030, 7, 10, 7, 10, 24).AddDays(30) };

                try
                {
                    Concediu concediuModif = repositoryConcediu.Update(newObj, 9).Result;
                    Assert.True(false); 
                }
                catch(Exception e )
                {
                    Assert.True(true);
                }

            }
        }
    }

   



    }
