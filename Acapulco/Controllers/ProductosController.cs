using Acapulco.Data;
using Acapulco.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acapulco.Controllers
{
    public class ProductosController : Controller
    {

        private readonly AcapulcoDbContext _context;

        public ProductosController(AcapulcoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoriaId)
        {
            
            var consulta = _context.Productos.Include(p => p.Categoria).AsQueryable();

            
            if (categoriaId.HasValue)
            {
                consulta = consulta.Where(p => p.CategoriaId == categoriaId.Value);
            }

            
            var modelo = new CatalogoViewModel
            {
                Productos = await consulta.ToListAsync(),
                Categorias = await _context.Categorias.ToListAsync(),
                CategoriaSeleccionadaId = categoriaId
            };

            return View(modelo);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
    }
}