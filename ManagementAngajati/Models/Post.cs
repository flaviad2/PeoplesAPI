using System.ComponentModel.DataAnnotations;
using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{
    public class Post : Entity<long>
    {


        [JsonProperty("Functie")]
        public String Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public String DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public String Departament { get; set; }

        public List<Angajat> IdAngajati { get; set; } = new List<Angajat>();

        public Post(long id, string functie, string detaliuFunctie, string departament, List<Angajat> angajati)
        {
            ID = id; 
            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            IdAngajati = angajati;
        }

        public Post()
        {

        }



    }
}
