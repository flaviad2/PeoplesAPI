using ManagementAngajati.Models.AngajatModel;
using ManagementAngajati.Models.PostModel;
using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.IstoricAngajatModel
{
    public class IstoricAngajatPostResponse : Entity<long>
    {
        [JsonProperty("IdAngajat")]
        public Angajat Angajat { get; set; }

        [JsonProperty("IdPost")]
        public PostResponse Post { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatPostResponse(long id, Angajat angajat, PostResponse post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            ID = id;
            Angajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatPostResponse()
        {

        }
    }
}
