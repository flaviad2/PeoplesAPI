using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class ConcediuRequest 
    { 
        [JsonProperty("IdAngajat")]
        public long Angajat { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public ConcediuRequest( long angajat, DateTime dataIncepere, DateTime dataTerminare)
        {
            Angajat = angajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public ConcediuRequest()
        {

        }
    }
}
