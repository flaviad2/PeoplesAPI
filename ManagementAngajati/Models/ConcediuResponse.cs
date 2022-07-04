using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class ConcediuResponse : Entity<long>
    
    {
        /* [Key]
         [JsonProperty("Id")]
         public int Id { get; set; } */

        [JsonProperty("IdAngajat")]
        public long Angajat { get; set; }   //id-ul angajatului

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public ConcediuResponse(long id, long angajat, DateTime dataIncepere, DateTime dataTerminare)
        {
            ID = id;
            Angajat = angajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public ConcediuResponse()
        {

        }
    }
}
