using Newtonsoft.Json;

namespace ManagementAngajati.Models
{

    [Serializable]

    public class Angajare : Entity<long>
    {

        [JsonProperty("IdAngajat")]
        public long IdAngajat { get; set; }

        [JsonProperty("IdPost")]
        public long IdPost { get; set; }


    }
}
