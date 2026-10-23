using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LaboratorioCRUDProductos.Models;


namespace LaboratorioCRUDProductos.Controllers
{
    public class ProductoController : Controller
    {
        // PRODUCTOS ALMACENADOS EN SESIÓN
        private List<ProductoModel> ObtenerProductos()
        {
            if (Session["productos"] == null)
            {
                Session["productos"] = new List<ProductoModel>
                {
                    new ProductoModel
                    {
                        Id = 1,
                        Nombre = "Laptop",
                        Descripcion = "Laptop Lenovo",
                        Precio = 2500,
                        Stock = 10
                    },
                    new ProductoModel
                    {
                        Id = 2,
                        Nombre = "Mouse",
                        Descripcion = "Mouse inalámbrico",
                        Precio = 50,
                        Stock = 20
                    },
                    new ProductoModel
                    {
                        Id = 3,
                        Nombre = "Teclado",
                        Descripcion = "Teclado mecánico",
                        Precio = 150,
                        Stock = 15
                    }
                };
            }


            return (List<ProductoModel>)Session["productos"];
        }


        // LISTAR PRODUCTOS
        public ActionResult Index()
        {
            return View(ObtenerProductos());
        }


        // CONSULTAR DETALLES
        [HttpGet]
        public ActionResult Details(int id)
        {
            var producto = ObtenerProductos()
                .FirstOrDefault(p => p.Id == id);


            if (producto == null)
                return HttpNotFound();


            return View(producto);
        }


        // MOSTRAR FORMULARIO DE REGISTRO
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        // GUARDAR PRODUCTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoModel producto)
        {
            if (ModelState.IsValid)
            {
                var productos = ObtenerProductos();


                producto.Id = productos.Count > 0
                    ? productos.Max(p => p.Id) + 1
                    : 1;


                productos.Add(producto);


                return RedirectToAction("Index");
            }


            return View(producto);
        }


        // MOSTRAR FORMULARIO DE EDICIÓN
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var producto = ObtenerProductos()
                .FirstOrDefault(p => p.Id == id);


            if (producto == null)
                return HttpNotFound();


            return View(producto);
        }


        // GUARDAR CAMBIOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoModel producto)
        {
            var existente = ObtenerProductos()
                .FirstOrDefault(p => p.Id == producto.Id);


            if (existente == null)
                return HttpNotFound();


            if (ModelState.IsValid)
            {
                existente.Nombre = producto.Nombre;
                existente.Descripcion = producto.Descripcion;
                existente.Precio = producto.Precio;
                existente.Stock = producto.Stock;


                return RedirectToAction("Index");
            }


            return View(producto);
        }


        // MOSTRAR CONFIRMACIÓN DE ELIMINACIÓN
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var producto = ObtenerProductos()
                .FirstOrDefault(p => p.Id == id);


            if (producto == null)
                return HttpNotFound();


            return View(producto);
        }


        // ELIMINAR PRODUCTO CONFIRMADO
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var productos = ObtenerProductos();


            var producto = productos
                .FirstOrDefault(p => p.Id == id);


            if (producto == null)
                return HttpNotFound();


            productos.Remove(producto);


            return RedirectToAction("Index");
        }
    }
}