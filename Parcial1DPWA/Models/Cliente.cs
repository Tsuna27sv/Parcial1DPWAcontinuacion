using System.ComponentModel.DataAnnotations;

namespace Parcial1DPWA.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Cliente> Clientes { get; set; }
    }
}
