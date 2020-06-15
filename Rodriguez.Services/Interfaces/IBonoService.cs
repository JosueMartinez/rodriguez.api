using Rodriguez.Data.Models;
using Rodriguez.Repo.Interfaces;
using System;
using System.Collections;
using static Rodriguez.Data.Utils.Constants;

namespace Rodriguez.Services.Interfaces
{
    public interface IBonoService //: IUnitOfWork
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
