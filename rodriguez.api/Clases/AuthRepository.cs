using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using rodriguez.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace rodriguez.api.Clases
{
    public class AuthRepository:IDisposable
    {
        private RodriguezModel db;
        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            db = new RodriguezModel();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
        }

        public async Task<IdentityResult> RegisterUser(Usuario userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.NombreUsuario
            };

            var result = await _userManager.CreateAsync(user, userModel.Contrasena);

            return result;
        }

        public async Task<IdentityResult> RegisterClient(Cliente cliente)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = cliente.Usuario
            };

            var result = await _userManager.CreateAsync(user, cliente.Password);

            return result;
        }



        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            db.Dispose();
            _userManager.Dispose();

        }

        
    }
}