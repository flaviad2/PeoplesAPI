using Newtonsoft.Json;

namespace ManagementAngajati.Models.AngajatModel
{
    public class AngajatRequest
    {

        [JsonProperty("Nume")]
        public string Nume { get; set; }

        [JsonProperty("Prenume")]
        public string Prenume { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("DataNasterii")]
        public DateTime DataNasterii { get; set; }

        [JsonProperty("Sex")]
        public string Sex { get; set; }

        [JsonProperty("Experienta")]
        public int Experienta { get; set; }

        [JsonProperty("IdPosturi")]

        public List<long> Posturi { get; set; } = new List<long>();
    }
}
