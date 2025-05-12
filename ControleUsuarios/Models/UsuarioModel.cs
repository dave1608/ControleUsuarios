using System.ComponentModel.DataAnnotations;
using ControleUsuarios.Enums;
using ControleUsuarios.Helper;

namespace ControleUsuarios.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o perfil do usuário!")]
        public PerfilEnum? Perfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Password == senha.GerarHash();
        }
        public void SetSenhaHash()
        {
            Password = Password.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Password = novaSenha.GerarHash();
        }
        public string GerarNovaSenha() 
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0 , 8);
            Password = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
