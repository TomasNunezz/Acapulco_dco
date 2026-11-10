namespace Acapulco.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public ICollection<CompraItem> Items { get; set; } = new List<CompraItem>();
    }
}