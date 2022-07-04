using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.Repository;
using ManagementAngajati.Utils;

namespace ManagementAngajati.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcediiController : ControllerBase
    {

        private IRepositoryConcediu _concediuData;
        private IRepositoryAngajat _angajatData; 

        public ConcediiController(IRepositoryConcediu concediuData, IRepositoryAngajat angajatData)
        {
            _concediuData = concediuData;
            _angajatData = angajatData;
        }

        [HttpGet]
        [Route("api/[controller]/concedii/{id}")]
        public IActionResult GetConcediu(long id)
        {
            var concediu = _concediuData.FindOne(id).Result;
            if(concediu != null )
            {
                return Ok(Converter.ConcediuToConcediuResponse(concediu));
            }
            return NotFound($"Concediul cu id: {id} nu a fost gasit!"); 
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAll()
        {
            var concedii = _concediuData.FindAll().Result;
            if(concedii != null)
            {
                return Ok(Converter.ConcediuListToConcediuResponseList(concedii)); 
            }
            return NoContent();
        }

        [HttpPost]
        [Route("api/concedii/[controller]")]
        public IActionResult PostConcecdiu(ConcediuRequest concediuRequest)
        {
            Concediu concediu = Converter.ConcediuW2ToConcediu(ConcediuRequestToW2(concediuRequest));
            Concediu added = _concediuData.Add(concediu).Result;
            //added are si id 

            return Ok(Converter.ConcediuToConcediuResponse(added));

        }


        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult EditConcediu(int id, ConcediuRequest concediuRequest)
        {
            Concediu concediu = Converter.ConcediuW2ToConcediu(ConcediuRequestToW2(concediuRequest));
            Concediu concediuModificat = _concediuData.Update(concediu, id).Result;

            if (concediuModificat != null)
                return Ok(Converter.ConcediuToConcediuResponse(concediuModificat));
            else
                return NotFound($"Concediu cu id {id} nu a fost gasit! "); 

        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteConcediu(int id)
        {
            var concediu = _concediuData.FindOne(id).Result;
            if(concediu!=null)
            {
                _concediuData.Delete(concediu.ID);
                return Ok();
            }

            return NotFound($"Concediul cu id {id} nu a fost gasit!"); 
        }

       
        //////////////////////////////////////////////////////////// Conversii ////////////////////////
        ///
        private ConcediuRequestW2 ConcediuRequestToW2 (ConcediuRequest concediuRequest)
        {
            Angajat aID = _angajatData.FindOne(concediuRequest.Angajat).Result;
            return new ConcediuRequestW2(aID, concediuRequest.DataIncepere, concediuRequest.DataTerminare); 

        }
    }
}
