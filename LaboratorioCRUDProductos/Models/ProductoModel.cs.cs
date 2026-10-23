using System.ComponentModel.DataAnnotations;


namespace LaboratorioCRUDProductos.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }


        [Range(0, 9999999, ErrorMessage = "El precio no puede ser negativo")]
        public decimal Precio { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }
    }
}