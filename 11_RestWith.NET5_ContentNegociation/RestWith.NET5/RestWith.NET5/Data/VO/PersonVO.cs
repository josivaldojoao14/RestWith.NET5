using System.Text.Json.Serialization;

namespace RestWith.NET5.Data.VO
{
    public class PersonVO
    {
        [JsonPropertyName("Código")]
        public long Id { get; set; }

        [JsonPropertyName("Primeiro nome")]
        public string FirstName { get; set; }

        [JsonPropertyName("Último nome")]
        public string LastName { get; set; }

        [JsonPropertyName("Endereço")]
        public string Address { get; set; }

        [JsonPropertyName("Sexo")]
        public string Gender { get; set; }
    }
}
