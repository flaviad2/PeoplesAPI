using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.Repository;
using ManagementAngajati.Utils;

namespace ManagementAngajati.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IstoricuriController : ControllerBase
    {

        private IRepositoryAngajat _angajatData;
        private IRepositoryIstoricAngajat _istoricData;
        private IRepositoryPost _postData;

        public IstoricuriController(IRepositoryAngajat angajatData, IRepositoryIstoricAngajat istoricData, IRepositoryPost postData)
        {
            _angajatData = angajatData;
            _istoricData = istoricData;
            _postData = postData;
        }

        [HttpGet]
        [Route("api/[controller]/idIstoric/{id}")]
        public IActionResult GetIstoric(long id)
        {
            var istoric = _istoricData.FindOne(id).Result; 
            if(istoric != null)
            {
                return Ok(Converter.IstoricToIstoricResponse(istoric));
            }
            return NotFound($"Istoricul cu id {id} nu a fost gasit! "); 
        }



        [HttpGet]
        [Route("api/[controller]/idAngajat/{id}")]
        public IActionResult GetIstoricByIdAngajat(long id)
        {
            var istoric = _istoricData.FindOneByIdAngajat(id).Result;
            if (istoric != null)
            {
                return Ok(Converter.IstoricToIstoricResponse(istoric));
            }
            return NotFound($"Istoricul pentru angajatul cu id {id} nu a fost gasit! ");
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAll()
        {
            var istoricuri = _istoricData.FindAll().Result; 
            if(istoricuri!= null)
            {
                return Ok(Converter.IstoricListToIstoricListResponse(istoricuri)); 

            }
            return NoContent(); 
        }


        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult EditIstoric(int id, IstoricAngajatRequest istoricAngajatRequest)
        {
            IstoricAngajat istoric = Converter.IstoricW2ToIstoric(IstoricRequestToW2(istoricAngajatRequest));
            IstoricAngajat istoricModificat = _istoricData.Update(istoric, id).Result;

            if (istoricModificat != null)
            {
                return Ok(Converter.IstoricToIstoricResponse(istoricModificat));
            }
            else return NotFound($"Istoricul cu id {id}  nu a fost gasit!");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostIstoric(IstoricAngajatRequest istoricAngajatRequest)
        {
            IstoricAngajat istoric = Converter.IstoricW2ToIstoric(IstoricRequestToW2(istoricAngajatRequest));
            IstoricAngajat added = _istoricData.Add(istoric).Result; 
            return Ok(Converter.IstoricToIstoricResponse(added));
        }



        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteIstoric(int id)
        {
            var istoric = _istoricData.FindOne(id).Result; 
            if(istoric!=null)
            {
                _istoricData.Delete(id);
                return Ok(); 
            }
            return NotFound($"Istoricul cu id {id} nu a fost gasit!"); 
        }

        /////////////////////////////////// Conversii /////////////////////////
        ///
        private IstoricAngajatRequestW2 IstoricRequestToW2 (IstoricAngajatRequest istoricAngajatRequest)
        {
            Angajat aID = _angajatData.FindOne(istoricAngajatRequest.Angajat).Result;
            Post pID = _postData.FindOne(istoricAngajatRequest.Post).Result;

            return new IstoricAngajatRequestW2(aID, pID, istoricAngajatRequest.DataAngajare, istoricAngajatRequest.Salariu, istoricAngajatRequest.DataReziliere); 
        }
    }
}
