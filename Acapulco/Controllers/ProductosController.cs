using Acapulco.Data;
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
  
        public async Task<IActionResult> Index()
        {
         
            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();

            
            return View(productos);
        }
    }
}