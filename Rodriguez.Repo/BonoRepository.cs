using Rodriguez.Common;
using Rodriguez.Data.Models;
using Rodriguez.Data.Utils;
using Rodriguez.Repo.Interfaces;
using System.Collections;
using System.Linq;

namespace Rodriguez.Repo
{
    public class BonoRepository : Repository<Bono>, IBonoRepository
    {
        public BonoRepository() : base(new RodriguezModel()) { }

        public IEnumerable Get(EstadosBonos estado)
        {
            var estadoDescription = estado.GetDescription();
            return _db.Bonos.Include("EstadoBono").Where(x => x.EstadoBono.Descripcion.Equals(estadoDescription));
        }
    }
}

