using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial1DPWA.Models
{
    public class Envio
    {
        [Key]
    public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required]
        public int DestinatarioId { get; set; }
        [ForeignKey("DestinatarioId")]
        public Destinatario Destinatario { get; set; }

        // Añade la relación con Paquete
        [Required]
        public int PaqueteId { get; set; }
        [ForeignKey("PaqueteId")]
        public Paquete Paquete { get; set; }

        [Required]
        public int EstadosEnvioId { get; set; }
        [ForeignKey("EstadosEnvioId")]
        public EstadosEnvio EstadosEnvio { get; set; }

        // --- NUEVOS CAMPOS PARA LA RÚBRICA ---

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Departamento de Destino")]
        public string DepartamentoDestino { get; set; } // Ej: San Salvador, San Miguel...

        [Required]
        [DataType(DataType.Currency)]

        [Column(TypeName = "decimal(18,2)")] // Esto asegura 2 decimales para los centavos
        public decimal MontoTotal { get; set; }
    }
}
