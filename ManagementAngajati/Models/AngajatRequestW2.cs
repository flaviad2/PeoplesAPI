
using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class AngajatRequestW2 : Entity<long>
    {
        public AngajatRequestW2(string nume, string prenume, string username, string password, DateTime dataNasterii, string sex, int experienta, List<Post> posturi)
        {
            Nume = nume;
            Prenume = prenume;
            Username = username;
            Password = password;
            DataNasterii = dataNasterii;
            Sex = sex;
            Experienta = experienta;
            Posturi = posturi;
        }

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


        public List<Post> Posturi { get; set; } = new List<Post>();
    }
}
