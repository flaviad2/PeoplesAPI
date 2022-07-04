using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class ConcediuRequestW2 : Entity<long>
    {
        

        [JsonProperty("Angajat")]
        public Angajat Angajat { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public ConcediuRequestW2(Angajat angajat, DateTime dataIncepere, DateTime dataTerminare)
        {
           
            Angajat = angajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public ConcediuRequestW2()
        {

        }
    }
}
