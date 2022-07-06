using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    //nu are ID, iar lista de Posturi e lista de IDs
    public class AngajatRequest
    {

        [JsonProperty("Nume")]
        public String Nume { get; set; }

        [JsonProperty("Prenume")]
        public String Prenume { get; set; }

        [JsonProperty("Username")]
        public String Username { get; set; }

        [JsonProperty("Password")]
        public String Password { get; set; }

        [JsonProperty("DataNasterii")]
        public DateTime DataNasterii { get; set; }

        [JsonProperty("Sex")]
        public String Sex { get; set; }

        [JsonProperty("Experienta")]
        public int Experienta { get; set; }

        [JsonProperty("IdPosturi")]

        public List<long> Posturi { get; set; } = new List<long>();
    }
}
