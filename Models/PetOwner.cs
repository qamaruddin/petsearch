using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PetSearch.Models
{
    public class PetOwner
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("age")]
        public long Age { get; set; }

        [JsonPropertyName("pets")]
        public IEnumerable<Pet> Pets { get; set; }
    }
}