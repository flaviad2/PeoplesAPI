using System.ComponentModel.DataAnnotations;
using ManagementAngajati.Models;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities
{

    public class PostEntity
    {

        [Key]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Functie")]

        public string Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public string DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public string Departament { get; set; }

        [JsonProperty("Angajati")]

        public List<AngajatEntity> Angajati { get; set; }



    }
}
