using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.ConcediuModel
{
    public class ConcediuGETRequest
    {
        [JsonProperty("IdAngajat")]
        public long IdAngajat { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public ConcediuGETRequest(long angajat, DateTime dataIncepere, DateTime dataTerminare)
        {
            IdAngajat = angajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public ConcediuGETRequest()
        {

        }
    }
}
