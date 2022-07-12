using Newtonsoft.Json;

namespace ManagementAngajati.Models.ConcediuModel
{
    public class ConcediuPOSTRequest
    {
        [JsonProperty("ID")]
        public long ID { get; set; }

        [JsonProperty("IdAngajat")]
        public long IdAngajat { get; set; }
        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public ConcediuPOSTRequest(long angajat, DateTime dataIncepere, DateTime dataTerminare)
        {

            IdAngajat = angajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public ConcediuPOSTRequest()
        {

        }




    }
}
