using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class IstoricAngajatGETRequest
    {

        [JsonProperty("IdAngajat")]
        public long IdAngajat { get; set; }

        [JsonProperty("IdPost")]
        public long Post { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatGETRequest( long angajat, long post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            
            IdAngajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatGETRequest()
        {

        }
    }
}
