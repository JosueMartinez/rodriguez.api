using Rodriguez.Data.Models;

namespace Rodriguez.Repo.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetClienteNombre(string usuario);
        void DisableUsuario(Usuario usuario);
    }
}
