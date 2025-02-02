using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aradork.Models
{
    public class AragonDorks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;  // Inicialización por defecto

        [Required(ErrorMessage = "El valor del dork es obligatorio")]
        public string DorkValue { get; set; } = string.Empty;  // Inicialización por defecto

        public string? Descripcion { get; set; }  // Hacer nullable con '?'
    }
}