using System.ComponentModel.DataAnnotations;

namespace ControleUsuarios.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o email do usuário")]
        public string Email { get; set; }
    }
}
