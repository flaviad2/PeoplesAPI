using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManagementAngajati.Models;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities

{

    public class IstoricAngajatEntity
    {
        [JsonProperty("Id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("Angajat")]


        public AngajatEntity Angajat { get; set; }


        [JsonProperty("Post")]


        public Post Post { get; set; }


        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatEntity(AngajatEntity angajat, Post post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            Angajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatEntity()
        {

        }



    }
}
