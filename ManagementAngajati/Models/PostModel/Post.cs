using System.ComponentModel.DataAnnotations;
using ManagementAngajati.Models.AngajatModel;
using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.PostModel

{
    public class Post : Entity<long>
    {


        [JsonProperty("Functie")]
        public string Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public string DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public string Departament { get; set; }

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
