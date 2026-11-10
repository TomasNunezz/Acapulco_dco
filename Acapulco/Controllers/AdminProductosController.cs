using Acapulco.Models;
using Acapulco.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Acapulco.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acapulco.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductosController : Controller
    {
        private readonly AcapulcoDbContext _context;

        public AdminProductosController(AcapulcoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();

            return View(productos);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var modelo = new ProductoFormViewModel
            {
                Categorias = await ObtenerCategoriasAsync()
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoFormViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Categorias = await ObtenerCategoriasAsync();
                return View(modelo);
            }

            var producto = new Producto
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                Precio = modelo.Precio,
                Stock = modelo.Stock,
                ImagenUrl = modelo.ImagenUrl,
                CategoriaId = modelo.CategoriaId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> ObtenerCategoriasAsync()
        {
            return await _context.Categorias
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre })
                .ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var modelo = new ProductoFormViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                ImagenUrl = producto.ImagenUrl,
                CategoriaId = producto.CategoriaId,
                Categorias = await ObtenerCategoriasAsync()
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductoFormViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Categorias = await ObtenerCategoriasAsync();
                return View(modelo);
            }

            var producto = await _context.Productos.FindAsync(modelo.Id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = modelo.Nombre;
            producto.Descripcion = modelo.Descripcion;
            producto.Precio = modelo.Precio;
            producto.Stock = modelo.Stock;
            producto.ImagenUrl = modelo.ImagenUrl;
            producto.CategoriaId = modelo.CategoriaId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}