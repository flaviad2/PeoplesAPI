using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class IstoricAngajatRequest
    {

        [JsonProperty("IdAngajat")]
        public long Angajat { get; set; }

        [JsonProperty("IdPost")]
        public long Post { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatRequest( long angajat, long post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            
            Angajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatRequest()
        {

        }
    }
}
