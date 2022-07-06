using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities

{
    public class PostEntity : Entity<long>
    {


        [JsonProperty("Functie")]

        public string Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public string DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public string Departament { get; set; }

        public List<AngajatEntity> Angajati { get; set; } = new List<AngajatEntity>();

        public PostEntity(long id, string functie, string detaliuFunctie, string departament, List<AngajatEntity> angajati)
        {
            ID = id;
            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            Angajati = angajati;
        }

        public PostEntity()
        {

        }



    }
}
