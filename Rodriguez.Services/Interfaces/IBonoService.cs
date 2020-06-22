using Rodriguez.Data.Models;
using Rodriguez.Data.Utils;
using System.Collections;

namespace Rodriguez.Services.Interfaces
{
    public interface IBonoService
    {
        IEnumerable Get(EstadosBonos estado);
        Bono Get(int id);
        IEnumerable GetBonosCliente(int clientId);
        void PagarBono(int bonoId);
        Bono AddBono(Bono bono);
        void DeleteBono(int bonoId);
        bool Exists(int id);
    }
}
