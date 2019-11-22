using System.Collections;
using System.Data.Entity;
using System.Linq;
using Rodriguez.Data.Models;
using Rodriguez.Repo.Interfaces;

namespace Rodriguez.Repo
{
    public class TasaRepository : Repository<TasaMoneda>, ITasaRepository
    {
        
        public void DisableTasa(int monedaId)
        {
            var Tasas = _db.TasasMonedas.Where(x => x.Moneda.Id == (monedaId));
            Tasas.ForEachAsync((TasaMoneda t) => {
                t.Activa = false;
                _db.Entry(t).State = EntityState.Modified;
            });
        }

        public IEnumerable GetHistorial(int monedaId)
        {
            return _db.TasasMonedas.Where(x => x.MonedaId == monedaId).OrderByDescending(x => x.Fecha).Take(10);
        }
        public TasaMoneda GetTasaMoneda(string simbolo)
        {
            var tasa = _db.TasasMonedas.Where(x => x.Moneda.Simbolo == simbolo).OrderByDescending(x => x.Fecha);
            return tasa.Include(m => m.Moneda).FirstOrDefault();
        }      

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TasaRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
