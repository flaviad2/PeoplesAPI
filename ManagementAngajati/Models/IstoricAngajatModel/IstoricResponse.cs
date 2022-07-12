using ManagementAngajati.Models.AngajatModel;
using ManagementAngajati.Models.PostModel;
using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.IstoricAngajatModel
{
    public class IstoricAngajatResponse : Entity<long>
    {
        [JsonProperty("IdAngajat")]
        public AngajatNoPostsResponse Angajat { get; set; }

        [JsonProperty("IdPost")]
        public PostResponse Post { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatResponse(long id, AngajatNoPostsResponse angajat, PostResponse post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            ID = id;
            Angajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatResponse()
        {

        }
    }
}
