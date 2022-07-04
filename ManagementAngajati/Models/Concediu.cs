using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{ 
    public class Concediu : Entity<long>
    {
       /* [Key]
        [JsonProperty("Id")]
        public int Id { get; set; } */

        [JsonProperty("Angajat")]
        public Angajat Angajat { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

     public Concediu(long id, Angajat angajat, DateTime dataIncepere, DateTime dataTerminare)
        {
        ID = id;
        Angajat = angajat;
        DataIncepere = dataIncepere;
        DataTerminare = dataTerminare;
     }

    public Concediu()
        {

        }
}
}
