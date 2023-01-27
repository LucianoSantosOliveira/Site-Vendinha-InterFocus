using Newtonsoft.Json;

namespace Site_Vendinha_InterFocus.Models
{
    public class Divida
    {
        public Guid DividaId { get; set; }
        public decimal ValorDivida { get; set; }
        public bool EstaPaga { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public DateTime DataDePagamento { get; set; }
        public Guid ClienteId { get; set; }
        [JsonIgnore]
        public string nomeCliente { get; set; }
    }
}
