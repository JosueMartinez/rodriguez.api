using Rodriguez.Data.Models;
using System;

namespace Rodriguez.Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TasaMoneda> Tasas { get; }
        TasaRepository TasasCustom { get; }
        IRepository<Usuario> Usuarios { get; }
        UsuarioRepository UsuariosCustom { get; }
        IRepository<Cliente> Clientes { get; }
        IRepository<Bono> Bonos { get; }
        IRepository<HistorialBono> Historiales { get; }
        IRepository<EstadoBono> Estados { get; }
        IRepository<Moneda> Monedas { get; }
        IRepository<Rol> Roles { get; }
        void Commit();
        void Dispose();
    }
}
