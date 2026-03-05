using Microsoft.AspNetCore.Identity;

namespace Parcial1DPWA.Models
{
    public class UsuariosApp : IdentityUser
    {
      
        public string? NombreCompleto { get; set; }
    }
}