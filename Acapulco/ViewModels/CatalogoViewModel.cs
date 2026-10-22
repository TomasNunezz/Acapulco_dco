using Acapulco.Models;

namespace Acapulco.ViewModels
{
    public class CatalogoViewModel
    {
       
        public List<Producto> Productos { get; set; } = new();

        public List<Categoria> Categorias { get; set; } = new();

        
        public int? CategoriaSeleccionadaId { get; set; }
    }
}