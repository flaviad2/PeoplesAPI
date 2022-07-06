using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence;
using ManagementAngajati.Persistence.DbUtils;
using ManagementAngajati.Persistence.Entities;
using ManagementAngajati.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;



namespace TestsManagementAngajati
{
    public class TestsAngajati
    {
        private DbContextOptions<ManagementAngajatiContext>? options;
        public TestsAngajati()
        {
         
        }
        
        public void SampleDataDB(DbContextOptions<ManagementAngajatiContext>? options)
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

                context.SaveChanges();
            }
        }


        [Fact]

        public void GetAllTest()
        {

            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
            .UseInMemoryDatabase(databaseName: "AngajatiDB11")
            .Options;

            SampleDataDB(options); 

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);
                List<Angajat> angajati = repoAngajat.FindAll().Result;

                Assert.Equal(3, angajati.Count);

                Assert.Equal(1, angajati[0].ID);
                Assert.Equal(2, angajati[1].ID);
                Assert.Equal(3, angajati[2].ID);

                Assert.Equal("Pop", angajati[0].Nume);
                Assert.Equal("Codreanu", angajati[1].Nume);
                Assert.Equal("Dorobat", angajati[2].Nume);


                Assert.Equal("Mihai", angajati[0].Prenume);
                Assert.Equal("Rares", angajati[1].Prenume);
                Assert.Equal("Flavia", angajati[2].Prenume);

                Assert.Equal("mita", angajati[0].Username);
                Assert.Equal("rares", angajati[1].Username);
                Assert.Equal("flavi", angajati[2].Username);


            }
        }


        
        [Fact]
        public async void GetByUsernamePasswordTests()
        {

            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
            .UseInMemoryDatabase(databaseName: "AngajatiDB22")
            .Options;

            SampleDataDB(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);
                Angajat angajat = await repoAngajat.GetAngajatByUsername("mita");

                Assert.Equal(1, angajat.ID);
                Assert.Equal("Pop", angajat.Nume);
                Assert.Equal("Mihai", angajat.Prenume);
                Assert.Equal("mita", angajat.Username);
                Assert.Equal("M", angajat.Sex);


                angajat = repoAngajat.GetAngajatByUsername("flavi").Result;
                Assert.Equal(3, angajat.ID);
                Assert.Equal("Dorobat", angajat.Nume);
                Assert.Equal("Flavia", angajat.Prenume);
                Assert.Equal("F", angajat.Sex);
                


            }
        }

        [Fact]
        public async Task AddAngajat_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "AngajatiDb3")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);
                Angajat angajat = await repoAngajat.Add(new Angajat {Username = "silvi", Nume = "Pirlea", Password = "parola4", Prenume = "Silvia", DataNasterii = DateTime.Now, Experienta = 2, IdPosturi = new List<Post>(), Sex = "F" });
                //pune id automat
                Assert.Equal(4, angajat.ID);
                Assert.Equal("Pirlea", angajat.Nume);
                Assert.Equal("Silvia", angajat.Prenume);
                Assert.Equal("silvi", angajat.Username);
                Assert.Equal("F", angajat.Sex);

                List<Angajat> angajati = repoAngajat.FindAll().Result; 
                Assert.Equal(4, angajati.Count);


            }
        }

        [Fact]
        public async Task AddAngajat_InvalidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "AngajatiDb4")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                try
                {
                    Angajat angajat = await repoAngajat.Add(new Angajat { ID = 1, Username = "silvi", Nume = "Pirlea", Password = "parola4", Prenume = "Silvia", DataNasterii = DateTime.Now, Experienta = 2, IdPosturi = new List<Post>(), Sex = "F" });
                    //avem deja acest id
                    Assert.True(false); 
                }
                catch(Exception e)
                {
                    Assert.True(true);
                }

            }
        }


        [Fact]
        public async Task DeleteAngajat_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "AngajatiDb5")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                repoAngajat.Delete(1);
                repoAngajat.Delete(2);

                List<Angajat> angajati = repoAngajat.FindAll().Result; 

                Assert.Equal(1, angajati.Count);

                Assert.Equal(3, angajati[0].ID);
                Assert.Equal("Flavia", angajati[0].Prenume);
                Assert.Equal("Dorobat", angajati[0].Nume);
                Assert.Equal("flavi", angajati[0].Username);
                Assert.Equal("F", angajati[0].Sex); 

            }
        }


        [Fact]
        public async Task DeleteAngajat_NoEffect()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "AngajatiDb6")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                repoAngajat.Delete(5);
                repoAngajat.Delete(6);

                List<Angajat> angajati = repoAngajat.FindAll().Result;

                Assert.Equal(3, angajati.Count);

                Assert.Equal(1, angajati[0].ID);
                Assert.Equal("Mihai", angajati[0].Prenume);
                Assert.Equal("Pop", angajati[0].Nume);
                Assert.Equal("mita", angajati[0].Username);
                Assert.Equal("M", angajati[0].Sex);

                Assert.Equal(2, angajati[1].ID);
                Assert.Equal("Rares", angajati[1].Prenume);
                Assert.Equal("Codreanu", angajati[1].Nume);
                Assert.Equal("rares", angajati[1].Username);
                Assert.Equal("M", angajati[1].Sex);

                Assert.Equal(3, angajati[2].ID);
                Assert.Equal("Flavia", angajati[2].Prenume);
                Assert.Equal("Dorobat", angajati[2].Nume);
                Assert.Equal("flavi", angajati[2].Username);
                Assert.Equal("F", angajati[2].Sex);

            }
        }



        [Fact]
        public async Task UpdateAngajat_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "AngajatiDb7")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                Angajat newObj =new Angajat { ID = 1, Username = "silvi", Nume = "Pirlea", Password = "parola4", Prenume = "Silvia", DataNasterii = DateTime.Now, Experienta = 2, IdPosturi = new List<Post>(), Sex = "F" };

                Angajat angajatModif = repoAngajat.Update(newObj, 1).Result;

                Assert.Equal(1, angajatModif.ID);
                Assert.Equal("silvi", angajatModif.Username);
                Assert.Equal("parola4", angajatModif.Password);
                Assert.Equal("Silvia", angajatModif.Prenume);
                Assert.Equal("Pirlea", angajatModif.Nume);
                Assert.Equal("F", angajatModif.Sex);
            }
        }

        [Fact]
        public async Task UpdateAngajat_NoEffectCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "AngajatiDb8")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryAngajat repoAngajat = new RepositoryAngajat(context);

                Angajat newObj = new Angajat { ID = 6, Username = "silvi", Nume = "Pirlea", Password = "parola4", Prenume = "Silvia", DataNasterii = DateTime.Now, Experienta = 2, IdPosturi = new List<Post>(), Sex = "F" };

                try
                {
                    Angajat? angajatModif = repoAngajat.Update(newObj, 6).Result;
                    Assert.True(false); 

                } 
                catch(Exception e)
                {
                    Assert.True(true);
                }

                

                List<Angajat> angajati = repoAngajat.FindAll().Result;



                Assert.Equal(3, angajati.Count);

                Assert.Equal(1, angajati[0].ID);
                Assert.Equal(2, angajati[1].ID);
                Assert.Equal(3, angajati[2].ID);

                Assert.Equal("Pop", angajati[0].Nume);
                Assert.Equal("Codreanu", angajati[1].Nume);
                Assert.Equal("Dorobat", angajati[2].Nume);


                Assert.Equal("Mihai", angajati[0].Prenume);
                Assert.Equal("Rares", angajati[1].Prenume);
                Assert.Equal("Flavia", angajati[2].Prenume);

                Assert.Equal("mita", angajati[0].Username);
                Assert.Equal("rares", angajati[1].Username);
                Assert.Equal("flavi", angajati[2].Username);
            }
        }



        


    }
}