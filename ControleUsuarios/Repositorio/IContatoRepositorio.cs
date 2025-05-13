using ControleUsuarios.Models;

namespace ControleUsuarios.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel BuscarPorId(int id);
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel Adicionar(ContatoModel usuario);
        ContatoModel Alterar(ContatoModel usuario);
        bool Deletar(int id);
    }
}
