using ManagementAngajati.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAngajati.Utils;
using ManagementAngajati.Models;

namespace ManagementAngajati.Controllers
{
    [Route("")]
    [ApiController]
    public class PosturiController : ControllerBase
    {
        private IRepositoryAngajat _angajatData;
        private IRepositoryPost _postData; 

        public PosturiController(IRepositoryAngajat angajatData, IRepositoryPost postData)
        {
            _angajatData = angajatData;
            _postData = postData;
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetPost(long id)
        {
            var post = _postData.FindOne(id).Result; 
            if(post!=null)
            {
                return Ok(Converter.PostToPostResponse(post)); 
            }
            return NotFound($"Postul cu id: {id} nu a fost gasit");
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAll()
        {
            var posturi = _postData.FindAll().Result;
            if(posturi!=null)
            {
                return Ok(Converter.PostListToPostResponseList(posturi)); 

            }
            return NoContent();
        }


        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostPost(PostRequest postRequest)
        {
            Post post = Converter.PostW2ToPost(PostRequestToW2(postRequest));
            Post added = _postData.Add(post).Result;

            //Daca postul are angajati, se adauga si la repo de angajati 
            List<Angajat> listAngajati = added.Angajati.ToList(); 

            //facem update la fiecare angajat, punand postul la care e arondat
            foreach(Angajat angajat in listAngajati)
            {
                List<Post> newPosturi = angajat.Posturi;
                if (!newPosturi.Contains(added))
                {
                    newPosturi.Add(added);
                    Angajat newAngajat = new Angajat(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, newPosturi);
                    _angajatData.Update(newAngajat, angajat.ID);
                }
            }
            return Ok(Converter.PostToPostResponse(added)); 
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult EditPost(int id, PostRequest postRequest)
        {
            Post post = Converter.PostW2ToPost(PostRequestToW2(postRequest));
            Post postVechi = _postData.FindOne(id).Result;
            Post postModificat = _postData.Update(post, id).Result;

            if (postModificat != null)
            {
                PostResponse postResponse = Converter.PostToPostResponse(postModificat);

                if (!Helper.IsEqualListOfAngajati(post.Angajati, postVechi.Angajati))
                {
                    List<Angajat> angajatiForUpdate = _angajatData.FindAll().Result;

                    foreach (Angajat angajat in angajatiForUpdate)
                    {
                        if (!angajat.Posturi.Contains(postModificat) && postModificat.Angajati.Contains(angajat))
                        {
                            List<Post> posturiAngajat = angajat.Posturi;
                            posturiAngajat.Add(postModificat);
                            Angajat angajatModificat = new Angajat(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, posturiAngajat);
                            _angajatData.Update(angajatModificat, angajatModificat.ID);
                        }
                        else if (angajat.Posturi.Contains(postModificat) && !postModificat.Angajati.Contains(angajat))
                        {
                            List<Post> posturiAngajat = angajat.Posturi;
                            posturiAngajat.Remove(postModificat);
                            Angajat angajatModificat = new Angajat(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, posturiAngajat);
                            _angajatData.Update(angajatModificat, angajatModificat.ID);
                        }
                    }
                }
                return Ok(postResponse);
            }
            else return NotFound("Acest post nu a fost gasit!");
        }


        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _postData.FindOne(id).Result; 
            if(post!=null)
            {
                _postData.Delete(post.ID);
                //cand stergem un post din repo, trebuie sa il stergem de la toti angajatii la care e arondat 

                List<Angajat> angajatiPost = post.Angajati; 
                foreach(Angajat angajat in angajatiPost)
                {
                    List<Post> posturiDeModif = angajat.Posturi;
                    posturiDeModif.Remove(post);

                    Angajat angajatNou = new Angajat(angajat.ID, angajat.Nume, angajat.Prenume, angajat.Username, angajat.Password, angajat.DataNasterii, angajat.Sex, angajat.Experienta, posturiDeModif);
                    _angajatData.Update(angajatNou, angajat.ID); 
                }
                return Ok();
                


            }
            return NotFound($"Postul cu Id {id} nu a fost gasit!"); 
        }







        ////////////////////////////////////////////////////////////// Conversii //////////////////////////////////////
        private PostRequestW2 PostRequestToW2(PostRequest postRequest)
        {
            List<Angajat> angajati = new List<Angajat>(); 
            foreach(long i in postRequest.Angajati)
            {
                angajati.Add(_angajatData.FindOne(i).Result); 
            }
            return new PostRequestW2(postRequest.Functie, postRequest.DetaliuFunctie, postRequest.Departament, angajati); 
        }
    }
}
