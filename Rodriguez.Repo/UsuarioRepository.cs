﻿using Rodriguez.Data.Models;
using Rodriguez.Repo.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Rodriguez.Repo
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository():base(new RodriguezModel()) { }

        public Usuario GetClienteNombre(string usuario)
        {
            Usuario user = _db.Usuarios.Include("Rol").FirstOrDefault(x => x.NombreUsuario.ToLower().Equals(usuario.ToLower()) && x.Activo);
            return user;
        }

        public List<Usuario> GetUsuariosDetails()
        {
            return _db.Usuarios
                        .Include("Rol")
                        .Where(x => x.Activo)
                        .ToList();
        }

        public void DisableUsuario(Usuario usuario)
        {
            usuario.Activo = false;
            _db.Entry(usuario).State = EntityState.Modified;
        }
    }
}
