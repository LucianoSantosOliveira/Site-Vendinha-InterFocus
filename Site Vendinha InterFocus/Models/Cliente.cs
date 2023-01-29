using Newtonsoft.Json;

namespace Site_Vendinha_InterFocus.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string ClienteName { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public decimal ValorTotalDividas { get; set; }
    }
}
