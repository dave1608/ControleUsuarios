using ControleUsuarios.Data;
using ControleUsuarios.Models;

namespace ControleUsuarios.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataBaseContext _dataBaseContext;

        public UsuarioRepositorio(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public UsuarioModel BuscarPorId(int id)
        {
            return _dataBaseContext.Usuarios.FirstOrDefault(u => u.Id == id);
        }
        public List<UsuarioModel> BuscarTodos()
        {
            return _dataBaseContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            // Inserir no banco de dados
            _dataBaseContext.Usuarios.Add(usuario);
            _dataBaseContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel Alterar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);

            if (usuarioDB == null) throw new System.Exception("Erro ao editar o usuário!");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _dataBaseContext.Usuarios.Update(usuarioDB);
            _dataBaseContext.SaveChanges();

            return usuarioDB;
        }

        public bool Deletar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Erro ao deletar o usuário!");

            _dataBaseContext.Usuarios.Remove(usuarioDB);
            _dataBaseContext.SaveChanges();

            return true;
        }
    }
}