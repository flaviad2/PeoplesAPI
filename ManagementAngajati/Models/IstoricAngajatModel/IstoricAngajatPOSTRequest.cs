using Newtonsoft.Json;

namespace ManagementAngajati.Models.IstoricAngajatModel
{
    public class IstoricAngajatPOSTRequest
    {
        [JsonProperty("ID")]
        public long ID { get; set; }

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


        public IstoricAngajatPOSTRequest(long id, long angajat, long post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
        {
            ID = ID;
            IdAngajat = angajat;
            Post = post;
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
        }
        public IstoricAngajatPOSTRequest()
        {
        }
    }
}
