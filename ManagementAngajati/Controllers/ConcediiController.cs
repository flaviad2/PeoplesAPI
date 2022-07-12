using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.Repository;
using ManagementAngajati.Utils;
using ManagementAngajati.Persistence.Entities;

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
        public async Task<IActionResult> GetConcediuAsync(long id)
        {
            var concediu = await _concediuData.FindOne(id);
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
        public async Task<IActionResult> PostConcecdiuAsync(ConcediuPOSTRequest concediuRequest) 
        {
            Angajat angajatConcediu = await _angajatData.FindOne(concediuRequest.IdAngajat);
            if (angajatConcediu == null)
                return BadRequest("Angajatul cu acest id nu exista!"); 
            try
            {   
                Concediu concediu = ConcediuPostRequestToConcediu(concediuRequest);
                Concediu added = await _concediuData.Add(concediu);
                return Ok(Converter.ConcediuToConcediuResponse(added));
            }
           

            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 


        }


        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditConcediuAsync(int id, ConcediuPOSTRequest concediuRequest)
        {
            try { 
          

                Concediu concediuModificat = await _concediuData.Update(ConcediuPostRequestToConcediu(concediuRequest), id);

                if (concediuModificat != null)
                    return Ok(Converter.ConcediuToConcediuResponse(concediuModificat));
                else
                  return NotFound($"Concediu cu id {id} nu a fost gasit! ");
           }
           catch (KeyNotFoundException e)
            {

                return BadRequest(e.Message);
            }

            catch (Exception e)
             {

                 return BadRequest(e.Message);
             }

        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteConcediu(int id)
        {
            try
            {
                var concediu = _concediuData.FindOne(id).Result;
                if (concediu != null)
                {
                    _concediuData.Delete(concediu.ID);
                    return Ok();
                }

                return NotFound($"Concediul cu id {id} nu a fost gasit!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
        //////////////////////////////////////////////////////////// Conversii ////////////////////////
        ///
        private Concediu ConcediuPostRequestToConcediu  (ConcediuPOSTRequest concediuRequest)
        {
            Angajat aID = _angajatData.FindOne(concediuRequest.IdAngajat).Result;
            return new Concediu(concediuRequest.ID, aID, concediuRequest.DataIncepere, concediuRequest.DataTerminare); 

        }
    }
}
