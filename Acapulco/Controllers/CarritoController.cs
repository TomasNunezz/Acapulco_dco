using System.Security.Claims;
using Acapulco.Data;
using Acapulco.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acapulco.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly AcapulcoDbContext _context;

        public CarritoController(AcapulcoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = await _context.CarritoItems
                .Include(i => i.Producto)
                .Where(i => i.UsuarioId == usuarioId)
                .ToListAsync();

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(int productoId, int cantidad = 1)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null)
            {
                return NotFound();
            }

            if (producto.Stock <= 0)
            {
                return RedirectToAction("Detalle", "Productos", new { id = productoId });
            }

            if (cantidad < 1)
            {
                cantidad = 1;
            }

            var item = await _context.CarritoItems
                .FirstOrDefaultAsync(i => i.UsuarioId == usuarioId && i.ProductoId == productoId);

            if (item == null)
            {
                item = new CarritoItem
                {
                    UsuarioId = usuarioId,
                    ProductoId = productoId,
                    Cantidad = Math.Min(cantidad, producto.Stock)
                };
                _context.CarritoItems.Add(item);
            }
            else
            {
                item.Cantidad = Math.Min(item.Cantidad + cantidad, producto.Stock);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Productos");
        }

        [HttpPost]
        public async Task<IActionResult> CambiarCantidad(int id, int cantidad)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var item = await _context.CarritoItems
                .Include(i => i.Producto)
                .FirstOrDefaultAsync(i => i.Id == id && i.UsuarioId == usuarioId);

            if (item == null)
            {
                return NotFound();
            }

            if (cantidad < 1)
            {
                cantidad = 1;
            }
            if (cantidad > item.Producto.Stock)
            {
                cantidad = item.Producto.Stock;
            }

            item.Cantidad = cantidad;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Quitar(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var item = await _context.CarritoItems
                .FirstOrDefaultAsync(i => i.Id == id && i.UsuarioId == usuarioId);

            if (item != null)
            {
                _context.CarritoItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Confirmar()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = await _context.CarritoItems
                .Include(i => i.Producto)
                .Where(i => i.UsuarioId == usuarioId)
                .ToListAsync();

            if (!items.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var i in items)
            {
                if (i.Cantidad > i.Producto.Stock)
                {
                    TempData["Error"] = $"No hay stock suficiente de {i.Producto.Nombre} (disponible: {i.Producto.Stock}).";
                    return RedirectToAction(nameof(Index));
                }
            }

            var compra = new Compra
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                Total = items.Sum(i => i.Cantidad * i.Producto.Precio)
            };

            foreach (var i in items)
            {
                compra.Items.Add(new CompraItem
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.Producto.Precio
                });
                i.Producto.Stock -= i.Cantidad;
            }

            _context.Compras.Add(compra);
            _context.CarritoItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Gracias), new { id = compra.Id });
        }

        public async Task<IActionResult> Gracias(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var compra = await _context.Compras
                .Include(c => c.Items)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);

            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

    }
}