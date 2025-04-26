using ControleUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleUsuarios.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contatos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set;}
    }
}