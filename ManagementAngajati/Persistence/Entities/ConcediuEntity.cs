using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence.Entities
{


    public class ConcediuEntity
    {
        [Key]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }


    }
}
