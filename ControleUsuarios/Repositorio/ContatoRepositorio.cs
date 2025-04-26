using ControleUsuarios.Data;
using ControleUsuarios.Models;

namespace ControleUsuarios.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly DataBaseContext _dataBaseContext;

        public ContatoRepositorio(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public ContatoModel BuscarPorId(int id)
        {
            return _dataBaseContext.Contatos.FirstOrDefault(u => u.Id == id);
        }
        public List<ContatoModel> BuscarTodos()
        {
            return _dataBaseContext.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel usuario)
        {
            // Inserir no banco de dados
            _dataBaseContext.Contatos.Add(usuario);
            _dataBaseContext.SaveChanges();

            return usuario;
        }

        public ContatoModel Alterar(ContatoModel usuario)
        {
            ContatoModel usuarioDB = BuscarPorId(usuario.Id);

            if (usuarioDB == null) throw new System.Exception("Erro ao editar o contato!");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Celular = usuario.Celular;

            _dataBaseContext.Contatos.Update(usuarioDB);
            _dataBaseContext.SaveChanges();

            return usuarioDB;
        }

        public bool Deletar(int id)
        {
            ContatoModel usuarioDB = BuscarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Erro ao deletar o contato!");

            _dataBaseContext.Contatos.Remove(usuarioDB);
            _dataBaseContext.SaveChanges();

            return true;
        }
    }
}