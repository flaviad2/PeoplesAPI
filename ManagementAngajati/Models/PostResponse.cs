using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{
    public class PostResponse : Entity<long>
    {

        [JsonProperty("Functie")]

        public String Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public String DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public String Departament { get; set; }

        [JsonProperty("IdAngajati")]
        public List<long> Angajati { get; set; } = new List<long>();

        public PostResponse(long id, string functie, string detaliuFunctie, string departament, List<long> angajati)
        {
            ID = id;
            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            Angajati = angajati;
        }

        public PostResponse()
        {

        }



    }
}
