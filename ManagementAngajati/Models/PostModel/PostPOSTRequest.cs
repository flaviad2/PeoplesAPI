using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.PostModel

{
    public class PostPOSTRequest
    {

        [JsonProperty("ID")]

        public long ID { get; set; }

        [JsonProperty("Functie")]

        public string Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public string DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public string Departament { get; set; }

        [JsonProperty("IdAngajati")]
        public List<long> IdAngajati { get; set; } = new List<long>();

        public PostPOSTRequest(string functie, string detaliuFunctie, string departament, List<long> angajati)
        {

            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            IdAngajati = angajati;
        }

        public PostPOSTRequest()
        {

        }



    }
}
