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
    public class TestsPosturi
    {
        private DbContextOptions<ManagementAngajatiContext>? options;
        public TestsPosturi()
        {

        }

        public void SampleDataDB(DbContextOptions<ManagementAngajatiContext>? options)
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

                context.SaveChanges();
            }
        }


        [Fact]

        public void GetAllPostsTest()
        {

            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
            .UseInMemoryDatabase(databaseName: "Posturi1")
            .Options;

            SampleDataDB(options);

            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryPost repoPosturi = new RepositoryPost(context); 

                List<Post> posturi = repoPosturi.FindAll().Result;

                Assert.Equal(3, posturi.Count);

                Assert.Equal(1, posturi[2].ID);
                Assert.Equal(2, posturi[0].ID);
                Assert.Equal(3, posturi[1].ID);

                Assert.Equal("Tester", posturi[2].Functie);
                Assert.Equal("Scrum master", posturi[0].Functie);
                Assert.Equal("Programator", posturi[1].Functie);

                Assert.Equal("Digital", posturi[2].Departament);
                Assert.Equal("Digital", posturi[0].Departament);
                Assert.Equal("Digital", posturi[1].Departament);
              
            }
        }



       
        [Fact]
        public async Task AddPost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "PosturiDb2")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                //pune id automat
             

                RepositoryPost repoPosturi = new RepositoryPost(context);
                Post post = await repoPosturi.Add(new Post { Functie = "Functie noua", Departament = "Digital", Angajati = new List<Angajat>(), DetaliuFunctie = "Detalii functie" }); 

                Assert.Equal(4, post.ID);
                Assert.Equal("Functie noua", post.Functie);
                Assert.Equal("Digital", post.Departament);
                Assert.Equal("Detalii functie", post.DetaliuFunctie); 


            }
        }

        [Fact]
        public async Task AddPost_InvalidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "PosturiDb3")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                //pune id automat


                RepositoryPost repoPosturi = new RepositoryPost(context);
                try
                {
                    Post post = await repoPosturi.Add(new Post {ID=1, Functie = "Functie noua", Departament = "Digital", Angajati = new List<Angajat>(), DetaliuFunctie = "Detalii functie" });
                    Assert.True(false);
                }
                catch(Exception e)
                {
                    Assert.True(true); 
                }
               

            }
        }


        [Fact]
        public async Task DeletePost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "PosturiDb44")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {

                RepositoryPost repoPost = new RepositoryPost(context); 

                Post p1 = await repoPost.Delete(1);
                Post p2 = await repoPost.Delete(2);

                List<Post> posturi = repoPost.FindAll().Result;

                Assert.Equal(1, posturi.Count);

                Assert.Equal(3, posturi[0].ID);
           
                Assert.Equal("Programator", posturi[0].Functie);

                Assert.Equal("Digital", posturi[0].Departament);


            }
        }


        [Fact]
        public async Task DeletePost_NoEffect()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "PosturiDb5")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {

                RepositoryPost repoPost = new RepositoryPost(context);

                repoPost.Delete(8);
                repoPost.Delete(9);

                List<Post> posturi = repoPost.FindAll().Result;

                Assert.Equal(3, posturi.Count);

               

            }
        }

    

       [Fact]
        public async Task UpdatePost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "PosturiDb66")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {

                RepositoryPost repoPost = new RepositoryPost(context); 

                Post newObj = new Post  { ID = 1, Functie = "Functie noua", Departament = "Digital", Angajati = new List<Angajat>(), DetaliuFunctie = "Detalii functie" };

                Post postModificat = repoPost.Update(newObj, 1).Result;

                Assert.Equal(1, postModificat.ID);
                Assert.Equal("Functie noua", postModificat.Functie);
                Assert.Equal("Digital", postModificat.Departament);
                Assert.Equal("Detalii functie", postModificat.DetaliuFunctie);

                List<Post> posturi = repoPost.FindAll().Result;

                Assert.Equal(1, posturi[2].ID);
                Assert.Equal("Functie noua", posturi[2].Functie);
                Assert.Equal("Digital", posturi[2].Departament);
                Assert.Equal("Detalii functie", posturi[2].DetaliuFunctie);


            }
        }

        [Fact]
        public async Task UpdateAngajat_NoEffectCall()
        {
            var options = new DbContextOptionsBuilder<ManagementAngajatiContext>()
                    .UseInMemoryDatabase(databaseName: "PosturiDb7")
                    .Options;

            SampleDataDB(options);


            //clean instance of context 
            using (var context = new ManagementAngajatiContext(options))
            {
                RepositoryPost repoPosturi = new RepositoryPost(context);


                Post newObj = new Post { ID = 8, Functie = "Functie noua", Departament = "Digital", Angajati = new List<Angajat>(), DetaliuFunctie = "Detalii functie" };


                try
                {
                    Post? postModif = repoPosturi.Update(newObj, 1).Result;
                    Assert.True(false);

                }
                catch (Exception e)
                {
                    Assert.True(true);
                }


            }
        }



    }
}
