using ManagementAngajati.Models;
using ManagementAngajati.Persistence.Entities;
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
        public async Task<IActionResult> GetAngajatAsync(long id)
        {
            var angajat = await _angajatData.FindOne(id);
            if (angajat != null)
            {
                return Ok(Converter.AngajatToAngajatResponse(angajat));
            }
            return NotFound($"Angajatul cu id: {id} nu a fost gasit");

        }

        [HttpGet]
        [Route("api/[controller]/username/{username}")]
        public IActionResult GetAngajatLogIn(String username)
        {
            var angajat = _angajatData.GetAngajatByUsername(username).Result;
            if (angajat != null)
            {
                return Ok(Converter.AngajatToAngajatResponse(angajat));
            }
            return NotFound($"Angajatul cu username: {username} nu a fost gasit");

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
        public async Task<IActionResult> PostAngajatAsync(AngajatPOSTRequest angajatRequest)
        {
             try
             {

            Angajat angajat = AngajatPostRequestToAngajat(angajatRequest);
            Angajat added = _angajatData.Add(angajat).Result;


            List<Post> listPosts = added.IdPosturi.ToList();

            foreach (Post post in listPosts)
            {
                List<Angajat> newAngajati = post.IdAngajati;
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            } 
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditAngajatAsync(int id, AngajatPOSTRequest angajatRequest)
        {
            try
            {
                Angajat angajat = AngajatPostRequestToAngajat(angajatRequest);
                Angajat angajatVechi = await _angajatData.FindOne(id);
                Angajat angajatModificat = _angajatData.Update(angajat, id).Result;


                if (angajatModificat != null)
                {

                    AngajatResponse angajatResponse = Converter.AngajatToAngajatResponse(angajat);

                    //daca s-a schimbat lista de posturi pt acest angajat, trebuie facute modificari si in repo-ul de posturi
                    if (!Helper.IsEqualListOfPosts(angajat.IdPosturi, angajatVechi.IdPosturi))
                    {

                        List<Post> postsForUpdate = _postData.FindAll().Result;

                        foreach (Post post in postsForUpdate)
                        {
                            if (!post.IdAngajati.Contains(angajatModificat) && angajatModificat.IdPosturi.Contains(post))
                            {
                                List<Angajat> angajatiPost = post.IdAngajati;
                                angajatiPost.Add(angajatModificat);
                                Post postModificat = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajatiPost);
                                _postData.Update(postModificat, postModificat.ID);

                            }
                            else if (post.IdAngajati.Contains(angajatModificat) && !angajatModificat.IdPosturi.Contains(post))
                            {
                                List<Angajat> angajatiPost = post.IdAngajati;
                                angajatiPost.Remove(angajatModificat);
                                Post postModificat = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajatiPost);
                                _postData.Update(postModificat, postModificat.ID);
                            }
                        }

                    }

                    return Ok(angajatResponse);
                }
                return NotFound("Acest angajat nu a fost gasit");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }



        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteAngajat(int id)
        {
            try
            {
                var angajat = _angajatData.FindOne(id).Result;
                if (angajat != null)
                {
                    _angajatData.Delete(angajat.ID);

                    //cand stergem un angajat din repo, trebuie sa il stergem si din toate posturile la care e arondat

                    List<Post> posturiAngajat = angajat.IdPosturi;
                    foreach (Post post in posturiAngajat)
                    {
                        List<Angajat> angajatiDeModif = post.IdAngajati;
                        angajatiDeModif.Remove(angajat);

                        Post postNou = new Post(post.ID, post.Functie, post.DetaliuFunctie, post.Departament, angajatiDeModif);
                        _postData.Update(postNou, post.ID);

                    }
                    return Ok();
                }
                return NotFound($"Angajatul cu Id: {id} nu a fost gasit!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        ////////////////////////////////////////////////// CONVERSII //////////////////////////////////////////
        private Angajat AngajatPostRequestToAngajat(AngajatPOSTRequest angajatRequest)
        {
            List<Post> posturi = new List<Post>();
            foreach (long i in angajatRequest.Posturi)
            {
                posturi.Add(_postData.FindOne(i).Result);
            }
            return new Angajat(angajatRequest.ID, angajatRequest.Nume, angajatRequest.Prenume, angajatRequest.Username,
                            angajatRequest.Password, angajatRequest.DataNasterii, angajatRequest.Sex, angajatRequest.Experienta,
                            posturi);
        }
    }
}
