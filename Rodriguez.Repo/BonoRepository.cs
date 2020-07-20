using Rodriguez.Common;
using Rodriguez.Data.Models;
using Rodriguez.Data.Utils;
using Rodriguez.Repo.Interfaces;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace Rodriguez.Repo
{
    public class BonoRepository : Repository<Bono>, IBonoRepository
    {
        public BonoRepository() : base(new RodriguezModel()) { }

        public IEnumerable Get(EstadosBonos estado)
        {
            var estadoDescription = estado.GetDescription();
            return _db.Bonos
                .Include(x => x.EstadoBono)
                .Include(x => x.Cliente)
                .Include(x => x.Tasa)
                .Include(x => x.Tasa.Moneda)
                .Where(x => x.EstadoBono.Descripcion.Equals(estadoDescription));
        }

        public Bono Get(int id)
        {
            return _db.Bonos
                .Include(x => x.EstadoBono)
                .Include(x => x.Cliente)
                .Include(x => x.Tasa)
                .Include(x => x.Tasa.Moneda)
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable GetClient(int clientId)
        {
            return _db.Bonos
                .Include(x => x.EstadoBono)
                .Include(x => x.Cliente)
                .Include(x => x.Tasa)
                .Include(x => x.Tasa.Moneda)
                .Where(x => x.ClienteId.Equals(clientId))
                .OrderByDescending(x => x.FechaCompra);
        }
    }
}

