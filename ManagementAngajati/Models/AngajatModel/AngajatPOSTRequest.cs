using Newtonsoft.Json;

namespace ManagementAngajati.Models.AngajatModel
{
    public class AngajatPOSTRequest
    {

        [JsonProperty("ID")]

        public long ID { get; set; }

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

        public AngajatPOSTRequest(long iD, string nume, string prenume, string username, string password, DateTime dataNasterii, string sex, int experienta, List<long> posturi)
        {
            ID = iD;
            Nume = nume;
            Prenume = prenume;
            Username = username;
            Password = password;
            DataNasterii = dataNasterii;
            Sex = sex;
            Experienta = experienta;
            Posturi = posturi;
        }

    }
}
