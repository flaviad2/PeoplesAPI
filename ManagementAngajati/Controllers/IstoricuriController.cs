using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.Repository;
using ManagementAngajati.Utils;
using ManagementAngajati.Persistence.Entities;

namespace ManagementAngajati.Controllers
{
    [Route("")]
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
        [Route("api/EmployeesHistories")]
        public IActionResult GetAll()
        {
            var istoricuri = _istoricData.FindAll().Result;
            if (istoricuri != null)
            {
                return Ok(Converter.IstoricListToIstoricListResponse(istoricuri));

            }
            return NoContent();
        }

        [HttpGet]
        [Route("api/EmployeeHistory/{idEmployeeHistory}")]
        public IActionResult GetIstoric(long idEmployeeHistory)
        {
            var istoric = _istoricData.FindOne(idEmployeeHistory).Result; 
            if(istoric != null)
            {
                return Ok(Converter.IstoricToIstoricResponse(istoric));
            }
            return NotFound($"Istoricul cu id {idEmployeeHistory} nu a fost gasit! "); 
        }



        [HttpGet]
        [Route("api/EmployeeHistory/{idEmployee}")]
        public IActionResult GetIstoricByIdAngajat(long idEmployee)
        {
            var istoric = _istoricData.FindOneByIdAngajat(idEmployee).Result;
            if (istoric != null)
            {
                return Ok(Converter.IstoricToIstoricResponse(istoric));
            }
            return NotFound($"Istoricul pentru angajatul cu id {idEmployee} nu a fost gasit! ");
        }

    


        [HttpPut]
        [Route("api/EmployeeHistory/{id}")]
        public IActionResult EditIstoric(int id, IstoricAngajatPOSTRequest istoricAngajatRequest)
        {
            try
            {
                IstoricAngajat istoric = IstoricPostRequestToIstoric(istoricAngajatRequest);
                IstoricAngajat istoricModificat = _istoricData.Update(istoric, id).Result;

                if (istoricModificat != null)
                {
                    return Ok(Converter.IstoricToIstoricResponse(istoricModificat));
                }
                else return NotFound($"Istoricul cu id {id}  nu a fost gasit!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/EmployeeHistory")]
        public IActionResult PostIstoric(IstoricAngajatPOSTRequest istoricAngajatRequest)
        {
            try
            {
                IstoricAngajat istoric = IstoricPostRequestToIstoric(istoricAngajatRequest);
                IstoricAngajat added = _istoricData.Add(istoric).Result;
                return Ok(Converter.IstoricToIstoricResponse(added));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpDelete]
        [Route("api/EmployeeHistory/{id}")]
        public IActionResult DeleteIstoric(int id)
        {
            try
            {
                var istoric = _istoricData.FindOne(id).Result;
                if (istoric != null)
                {
                    _istoricData.Delete(id);
                    return Ok();
                }
                return NotFound($"Istoricul cu id {id} nu a fost gasit!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /////////////////////////////////// Conversii /////////////////////////
        ///
        private IstoricAngajat IstoricPostRequestToIstoric (IstoricAngajatPOSTRequest istoricAngajatRequest)
        {
            Angajat aID = _angajatData.FindOne(istoricAngajatRequest.IdAngajat).Result;
            Post pID = _postData.FindOne(istoricAngajatRequest.Post).Result;

            return new IstoricAngajat(istoricAngajatRequest.ID,aID, pID, istoricAngajatRequest.DataAngajare, istoricAngajatRequest.Salariu, istoricAngajatRequest.DataReziliere); 
        }
    }
}
