using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class IstoricAngajatRequestW2 : Entity<long>
    {
        [JsonProperty("Angajat")]
        public Angajat Angajat { get; set; }

        [JsonProperty("Post")]
        public Post Post { get; set; }

        [JsonProperty("DataAngajare")]
        public DateTime DataAngajare { get; set; }
        [JsonProperty("Salariu")]
        public int Salariu { get; set; }

        [JsonProperty("DataReziliere")]
        public DateTime? DataReziliere { get; set; }


        public IstoricAngajatRequestW2(Angajat angajat, Post post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            
            Angajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatRequestW2()
        {

        }
    }
}
