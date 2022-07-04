using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{
    public class PostRequest
    {

        [JsonProperty("Functie")]

        public String Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public String DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public String Departament { get; set; }

        [JsonProperty("IdAngajati")]
        public List<long> Angajati { get; set; } = new List<long>();

        public PostRequest(string functie, string detaliuFunctie, string departament, List<long> angajati)
        {
           
            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            Angajati = angajati;
        }

        public PostRequest()
        {

        }



    }
}
