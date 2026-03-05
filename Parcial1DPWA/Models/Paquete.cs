using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial1DPWA.Models
{
    public class Paquete
    {
        [Key]
        public int Id { get; set; }

        // Lo hacemos opcional para evitar errores al registrar paquetes nuevos
        public int? EnvioId { get; set; }

        [ForeignKey("EnvioId")]
        public Envio? Envio { get; set; }

        // CAMBIO: Agregamos Required y un nombre claro para la vista
        [Required(ErrorMessage = "Debe describir qué contiene el paquete")]
        [Display(Name = "Descripción del Contenido")]
        public string DescripcionContenido { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 999.99, ErrorMessage = "El peso debe ser mayor a 0")]
        public decimal Peso { get; set; }
    }
}