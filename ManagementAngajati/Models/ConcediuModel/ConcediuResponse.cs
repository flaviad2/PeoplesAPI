using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;
using ManagementAngajati.Models.AngajatModel;

namespace ManagementAngajati.Models.ConcediuModel
{
    public class ConcediuResponse : Entity<long>

    {




        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        [JsonProperty("IdAngajat")]
        public AngajatResponse Angajat { get; set; }

        public ConcediuResponse(long id, AngajatResponse angajat, DateTime dataIncepere, DateTime dataTerminare)
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
