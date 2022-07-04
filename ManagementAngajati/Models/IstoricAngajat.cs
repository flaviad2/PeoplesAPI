using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{
    public class IstoricAngajat : Entity<long>
    {
       /* [JsonProperty("Id")]
        [Key]
        public int Id { get; set; } */

        
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


     public IstoricAngajat(long id, Angajat angajat, Post post, DateTime dataAngajare, int salariu, DateTime? dataReziliere)
    {
            ID = id;
            Angajat = angajat;
            Post = post; 
            DataAngajare = dataAngajare;
            Salariu = salariu;
            DataReziliere = dataReziliere;
    }
    public IstoricAngajat()
        {

        }

}
}
