using System;
using Rodriguez.Data.Models;
using Rodriguez.Repo.Interfaces;

namespace Rodriguez.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private RodriguezModel _db;
        private Repository<TasaMoneda> _tasas;
        private TasaRepository _tasasCustom;
        private Repository<Usuario> _usuarios;
        private UsuarioRepository _usuariosCustom;
        private Repository<Cliente> _clientes;
        private Repository<Bono> _bonos;
        private Repository<HistorialBono> _historiales;
        private Repository<EstadoBono> _estados;
        private Repository<Moneda> _monedas;
        private Repository<Rol> _roles;

        public UnitOfWork()
        {
            _db = new RodriguezModel();
        }


        public IRepository<TasaMoneda> Tasas
        {
            get
            {
                return _tasas ?? (_tasas = new Repository<TasaMoneda>(_db));
            }
        }

        public IRepository<Usuario> Usuarios
        {
            get
            {
                return _usuarios ?? (_usuarios = new Repository<Usuario>(_db));
            }
        }

        public IRepository<Cliente> Clientes
        {
            get
            {
                return _clientes ?? (_clientes = new Repository<Cliente>(_db));
            }
        }

        public IRepository<Bono> Bonos
        {
            get
            {
                return _bonos ?? (_bonos = new Repository<Bono>(_db));
            }
        }

        public IRepository<HistorialBono> Historiales
        {
            get
            {
                return _historiales ?? (_historiales = new Repository<HistorialBono>(_db));
            }
        }

        public IRepository<EstadoBono> Estados
        {
            get
            {
                return _estados ?? (_estados = new Repository<EstadoBono>(_db));
            }
        }

        public IRepository<Moneda> Monedas
        {
            get
            {
                return _monedas ?? (_monedas = new Repository<Moneda>(_db));
            }
        }

        public IRepository<Rol> Roles
        {
            get
            {
                return _roles ?? (_roles = new Repository<Rol>(_db));
            }
        }

        public TasaRepository TasasCustom
        {
            get
            {
                return _tasasCustom ?? (_tasasCustom = new TasaRepository());
            }
        }

        public UsuarioRepository UsuariosCustom
        {
            get
            {
                return _usuariosCustom ?? (_usuariosCustom = new UsuarioRepository());
            }
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
