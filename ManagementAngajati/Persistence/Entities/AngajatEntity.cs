using System.ComponentModel.DataAnnotations;
using ManagementAngajati.Models;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities


{
    public class AngajatEntity
    {
        [Key]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nume")]
        public string Nume { get; set; }

        [JsonProperty("Prenume")]
        public string Prenume { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("DataNasterii")]
        public DateTime DataNasterii { get; set; }

        [JsonProperty("Sex")]
        public string Sex { get; set; }

        [JsonProperty("Experienta")]
        public int Experienta { get; set; }

        [JsonProperty("Posturi")]

        public List<PostEntity> Posturi { get; set; }



    }
}

