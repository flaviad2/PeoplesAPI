using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models
{

    [Serializable]
  
    public class Angajat: Entity<long>
    {

       /* [Key]
        [JsonProperty("Id")]
        public int Id { get; set; } */

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

        public Angajat(long id, string nume, string prenume, string username, string password, DateTime dataNasterii, string sex, int experienta, List<Post> posturi)
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

        public Angajat()
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is Angajat angajat &&
                   ID == angajat.ID &&
                   Nume == angajat.Nume &&
                   Prenume == angajat.Prenume &&
                   Username == angajat.Username &&
                   Password == angajat.Password &&
                   DataNasterii == angajat.DataNasterii &&
                   Sex == angajat.Sex &&
                   Experienta == angajat.Experienta &&
                   EqualityComparer<List<Post>>.Default.Equals(Posturi, angajat.Posturi);
        }
    }
}
