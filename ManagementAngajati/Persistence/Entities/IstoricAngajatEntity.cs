using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities

{
    public class IstoricAngajatEntity : Entity<long>
    {


        [JsonProperty("Angajat")]
        public AngajatEntity Angajat { get; set; }

        [JsonProperty("Post")]
        public PostEntity Post { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatEntity(long id, AngajatEntity angajat, PostEntity post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            ID = id;
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
