using System.ComponentModel.DataAnnotations;
using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Persistence
{

    [Serializable]
  
    public class AngajatEntity: Entity<long>
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

        public List<PostEntity> IdPosturi { get; set; } = new List<PostEntity>();

        public List<ConcediuEntity>? Concedii { get; set; } = new List<ConcediuEntity>();

        public IstoricAngajatEntity? IstoricAngajat; 
       

        public AngajatEntity()
        {

        }

        public AngajatEntity(long id, string nume, string prenume, string username, string password, DateTime dataNasterii, string sex, int experienta, List<PostEntity> posturi)
        {
            ID = id;
            Nume = nume;
            Prenume = prenume;
            Username = username;
            Password = password;
            DataNasterii = dataNasterii;
            Sex = sex;
            Experienta = experienta;
            IdPosturi = posturi;
            Concedii = new List<ConcediuEntity>();
            IstoricAngajat = null;
        }

        public override bool Equals(object? obj)
        {
            return obj is AngajatEntity angajat &&
                   ID == angajat.ID &&
                   Nume == angajat.Nume &&
                   Prenume == angajat.Prenume &&
                   Username == angajat.Username &&
                   Password == angajat.Password &&
                   DataNasterii == angajat.DataNasterii &&
                   Sex == angajat.Sex &&
                   Experienta == angajat.Experienta &&
                   EqualityComparer<List<PostEntity>>.Default.Equals(IdPosturi, angajat.IdPosturi) &&
                   EqualityComparer<List<ConcediuEntity>>.Default.Equals(Concedii, angajat.Concedii) &&
                   EqualityComparer<IstoricAngajatEntity>.Default.Equals(IstoricAngajat, angajat.IstoricAngajat);
        }
    }
}
