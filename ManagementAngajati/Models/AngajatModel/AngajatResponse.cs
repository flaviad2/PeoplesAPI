using ManagementAngajati.Models.PostModel;
using Newtonsoft.Json;

namespace ManagementAngajati.Models.AngajatModel
{
    public class AngajatResponse : Entity<long>
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
        public List<PostResponse> Posturi { get; set; } = new List<PostResponse>();


        public AngajatResponse(long id, string nume, string prenume, string username, string password, DateTime dataNasterii, string sex, int experienta, List<PostResponse> posturi)
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
