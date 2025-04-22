using ControleUsuarios.Models;

namespace ControleUsuarios.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Alterar(UsuarioModel usuario);
        bool Deletar(int id);
    }
}
