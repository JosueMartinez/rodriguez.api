using Rodriguez.Data.Models;
using Rodriguez.Data.Utils;
using System.Collections;

namespace Rodriguez.Repo.Interfaces
{
    public interface IBonoRepository
    {
        IEnumerable Get(EstadosBonos estado);

        Bono Get(int id);
    }
}
