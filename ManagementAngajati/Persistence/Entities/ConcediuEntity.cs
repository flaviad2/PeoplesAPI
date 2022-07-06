using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities

{
    public class ConcediuEntity : Entity<long>
    {


        [JsonProperty("Angajat")]
        public AngajatEntity IdAngajat { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public ConcediuEntity(long id, AngajatEntity idAngajat, DateTime dataIncepere, DateTime dataTerminare)
        {
            ID = id;
            IdAngajat = idAngajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public ConcediuEntity()
        {

        }
    }
}
