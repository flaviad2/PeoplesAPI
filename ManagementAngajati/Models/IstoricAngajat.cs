using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class IstoricAngajat : Entity<long>
    {
        [JsonProperty("Angajat")]
        public long IdAngajat { get; set; }

        [JsonProperty("Post")]
        public long IdPost { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajat(long id, long angajat, long post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            ID = id;
            IdAngajat = angajat;
            IdPost = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajat()
        {

        }
    }
}
