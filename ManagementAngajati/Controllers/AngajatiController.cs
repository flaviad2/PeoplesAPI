using ManagementAngajati.Models;
using ManagementAngajati.Persistence.Repository;
using ManagementAngajati.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAngajati.Controllers
{
    [Route("")]
    [ApiController]
    public class AngajatiController : ControllerBase
    {
        private IRepositoryAngajat _angajatData;

        private IRepositoryPost _postData; 

        public AngajatiController(IRepositoryAngajat repositoryAngajat, IRepositoryPost repositoryPost)
        {
            _angajatData = repositoryAngajat;
            _postData = repositoryPost;
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetAngajat(long id)
        {
            var angajat = _angajatData.FindOne(id).Result;
            if (angajat != null)
            {
                return Ok(Converter.AngajatToAngajatResponse(angajat));
            }
            return NotFound($"Angajatul cu id: {id} nu a fost gasit");

        }


        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAll()
        {
            var angajati = _angajatData.FindAll().Result;
            if (angajati != null)
            {
                return Ok(Converter.AngajatListToAngajatResponseList(angajati));
            }
            return NoContent();

        }

        
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostAngajat(AngajatRequest angajatRequest)
        {
            //fac doar o conversie? 
            //anagajatRequest nu are lista de posturi, ci lista de id-uri 
            //angajatRequestW2 nu are id, spre deosebire de Angajat care are id 
            // test

            Angajat angajat = Converter.AngajatW2ToAngajat(AngajatRequestToW2(angajatRequest));
            Angajat added = _angajatData.Add(angajat).Result; //are si id!!


            //Daca angajatul are posturi, se adauga si la posturi 
            List<Post> listPosts = added.Posturi.ToList();

            //Facem update la fiecare post, punand angajatul pe care il adaugam 
            foreach(Post post in listPosts)
            {
                List<Angajat> newAngajati = post.Angajati;
                if (!newAngajati.Contains(added))
                {
                    newAngajati.Add(added);
                    Post newPost = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, newAngajati);
                    _postData.Update(newPost, post.ID);
                }
            }

            AngajatResponse aResponse = Converter.AngajatToAngajatResponse(added); 
            
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + aResponse.ID, aResponse);

        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult EditAngajat(int id, AngajatRequest angajatRequest)
        {
            Angajat angajat = Converter.AngajatW2ToAngajat(AngajatRequestToW2(angajatRequest)); //angajatul, in forma de adaugat in repo
            Angajat angajatVechi = _angajatData.FindOne(id).Result;                             //angajatul cu id dat, inainte de modificare
            Angajat angajatModificat = _angajatData.Update(angajat, id).Result;                 //angajatul modificat, returnat de functia din repo


            if (angajatModificat != null)
            {
                //raspunsul pe care il dam la client: obiectul modif (daca au avut loc schimbari) 
                AngajatResponse angajatResponse = Converter.AngajatToAngajatResponse(angajat);

                //daca s-a schimbat lista de posturi pt acest angajat, trebuie facute modificari si in repo-ul de posturi
                if (!Helper.IsEqualListOfPosts(angajat.Posturi, angajatVechi.Posturi))
                {

                    List<Post> postsForUpdate = _postData.FindAll().Result;

                    foreach(Post post in postsForUpdate)
                    {
                        if(!post.Angajati.Contains(angajatModificat) && angajatModificat.Posturi.Contains(post))
                        {
                            List<Angajat> angajatiPost = post.Angajati;
                            angajatiPost.Add(angajatModificat); 
                            Post postModificat = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajatiPost);
                            _postData.Update(postModificat, postModificat.ID);

                        }
                        else if(post.Angajati.Contains(angajatModificat) && !angajatModificat.Posturi.Contains(post))
                        {
                            List<Angajat> angajatiPost = post.Angajati;
                            angajatiPost.Remove(angajatModificat);
                            Post postModificat = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajatiPost);
                            _postData.Update(postModificat, postModificat.ID);
                        }
                    }

                }

                return Ok(angajatResponse);
            } 
            else return NotFound("Acest angajat nu a fost gasit"); 
        }



        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteAngajat(int id)
        {
            var angajat = _angajatData.FindOne(id).Result;
            if(angajat!=null)
            {
                _angajatData.Delete(angajat.ID);

                //cand stergem un angajat din repo, trebuie sa il stergem si din toate posturile la care e arondat

                List<Post> posturiAngajat = angajat.Posturi; 
                foreach(Post post in posturiAngajat)
                {
                    List<Angajat> angajatiDeModif = post.Angajati;
                    angajatiDeModif.Remove(angajat);

                    Post postNou = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajatiDeModif);
                    _postData.Update(postNou, post.ID); 

                }
                return Ok();
            }
            return NotFound($"Angajatul cu Id: {id} nu a fost gasit!");
        }




        ////////////////////////////////////////////////// CONVERSII //////////////////////////////////////////
        private AngajatRequestW2 AngajatRequestToW2(AngajatRequest angajatRequest)
        {
            List<Post> posturi = new List<Post>();
            foreach (long i in angajatRequest.Posturi)
            {
                posturi.Add(_postData.FindOne(i).Result);
            }
            return new AngajatRequestW2(angajatRequest.Nume, angajatRequest.Prenume, angajatRequest.Username,
                            angajatRequest.Password, angajatRequest.DataNasterii, angajatRequest.Sex, angajatRequest.Experienta,
                            posturi);
        }
    }
}
