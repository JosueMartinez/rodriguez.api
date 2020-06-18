using Rodriguez.Data.Models;
using System.Collections;

namespace Rodriguez.Repo.Interfaces
{
    public interface ITasaRepository : IRepository<TasaMoneda>
    {
        IEnumerable GetHistorial(int monedaId);
        TasaMoneda GetTasaMoneda(string simbolo);
        void DisableTasa(int monedaId);
    }
}
