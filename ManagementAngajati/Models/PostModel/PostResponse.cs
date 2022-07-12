using System.ComponentModel.DataAnnotations;
using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.PostModel

{
    public class PostResponse : Entity<long>
    {

        [JsonProperty("Functie")]

        public string Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public string DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public string Departament { get; set; }

        [JsonProperty("IdAngajati")]
        private List<long> Angajati { get; set; } = new List<long>();


        public PostResponse()
        {

        }

        public PostResponse(long id, string functie, string detaliuFunctie, string departament)
        {
            ID = id;
            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
        }



    }
}
