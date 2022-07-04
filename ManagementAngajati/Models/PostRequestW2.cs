using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{
    public class PostRequestW2 : Entity<long>
    {


        [JsonProperty("Functie")]

        public String Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public String DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public String Departament { get; set; }

        public List<Angajat> Angajati { get; set; } = new List<Angajat>();

        public PostRequestW2( string functie, string detaliuFunctie, string departament, List<Angajat> angajati)
        {
           
            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            Angajati = angajati;
        }

        public PostRequestW2()
        {

        }



    }
}
