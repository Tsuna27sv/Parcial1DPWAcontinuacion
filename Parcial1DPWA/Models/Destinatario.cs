using System.ComponentModel.DataAnnotations;

namespace Parcial1DPWA.Models
{
    public class Destinatario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Telefono { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; } = null!;


        [Required]
        [StringLength(25)]
        public string Ciudad { get; set; } = null!;


        [Required]
        [StringLength(25)]
        public string Pais { get; set; } = null!;

        public ICollection<Destinatario> Destinatarios { get; set; } = new List<Destinatario>();
    }
}
