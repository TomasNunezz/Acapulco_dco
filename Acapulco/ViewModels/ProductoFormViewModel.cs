using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Acapulco.ViewModels
{
    public class ProductoFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        [Range(1, 100000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(0, 100000, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La foto es obligatoria")]
        [Display(Name = "Foto (URL)")]
        public string ImagenUrl { get; set; }

        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        public IEnumerable<SelectListItem>? Categorias { get; set; }
    }
}