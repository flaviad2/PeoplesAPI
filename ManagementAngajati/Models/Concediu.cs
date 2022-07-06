﻿using ManagementAngajati.Persistence.Entities;
using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    public class Concediu: Entity<long>
    {
       
        [JsonProperty("Angajat")]
        public long IdAngajat { get; set; }

        [JsonProperty("DataIncepere")]
        public DateTime DataIncepere { get; set; }


        [JsonProperty("DataTerminare")]
        public DateTime DataTerminare { get; set; }

        public Concediu(long id, long idAngajat, DateTime dataIncepere, DateTime dataTerminare)
        {
            ID = id;
            IdAngajat = idAngajat;
            DataIncepere = dataIncepere;
            DataTerminare = dataTerminare;
        }

        public Concediu()
        {

        }
    }
}
