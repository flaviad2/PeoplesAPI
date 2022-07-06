using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class AngajatResponse : Entity<long>
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


        public AngajatResponse(long id, string nume, string prenume, string username, string password, DateTime dataNasterii, string sex, int experienta, List<long> posturi)
        {
            ID = id;
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
