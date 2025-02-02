using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aradork.Models
{
    public class AragonDorks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Genera GUID automáticamente

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El valor del dork es obligatorio")]
        public string DorkValue { get; set; }

        public string Descripcion { get; set; }
    }
}