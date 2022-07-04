
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ManagementAngajati.Models
{
    [Serializable]
    public class Entity<TID>
    {
        [Key]
        [JsonProperty("ID")]

        public TID ID { get; set; }
    }
}