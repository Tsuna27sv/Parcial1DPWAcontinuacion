using System.ComponentModel.DataAnnotations;

namespace Parcial1DPWA.Models
{
    public class EstadosEnvio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreEstado { get; set; }

        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }

        public ICollection<EstadosEnvio> EstadosEnvios { get; set; }
    }
}
