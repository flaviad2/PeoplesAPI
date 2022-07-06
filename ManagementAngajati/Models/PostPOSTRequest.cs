﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models

{
    public class PostPOSTRequest
    {
        
        [JsonProperty("ID")]

        public long ID { get; set; }

        [JsonProperty("Functie")]

        public String Functie { get; set; }

        [JsonProperty("DetaliuFunctie")]
        public String DetaliuFunctie { get; set; }

        [JsonProperty("Departament")]
        public String Departament { get; set; }

        [JsonProperty("IdAngajati")]
        public List<long> IdAngajati { get; set; } = new List<long>();

        public PostPOSTRequest(string functie, string detaliuFunctie, string departament, List<long> angajati)
        {

            Functie = functie;
            DetaliuFunctie = detaliuFunctie;
            Departament = departament;
            IdAngajati = angajati;
        }

        public PostPOSTRequest()
        {

        }



    }
}