using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rodriguez.Data.Models;
using Rodriguez.Repo;
using Rodriguez.Services.Interfaces;
using static Rodriguez.Data.Utils.Constants;

namespace Rodriguez.Services
{
    public class BonoService : IBonoService
    {
        private readonly RodriguezModel _db;
        private readonly Repository<Bono> bonoRepo;

        public BonoService(RodriguezModel db)
        {
            _db = db;
            bonoRepo = new Repository<Bono>(_db);
        }

        public Bono AddBono(Bono bono)
        {
            try
            {
                InflateNewBono(bono);
                bonoRepo.Insert(bono);
                bonoRepo.Save();
                return bono;
            }
            catch (Exception)
            {
                return null;
            }

        }

        private void InflateNewBono(Bono bono)
        {
            bono.EstadoBonoId = getEstadoId(EstadosBonos.Comprado);
            bono.FechaCompra = DateTime.Now;
        }

        public void DeleteBono(int bonoId)
        {
            bonoRepo.Delete(bonoId);
            bonoRepo.Save();
        }

        public bool Exists(int id)
        {
            var result = false;
            result = bonoRepo.Get(id) != null;
            return result;
        }

        public IEnumerable Get(EstadosBonos estado)
        {
            return bonoRepo.Get()
                    .Where(x => x.EstadoBono.Descripcion.Equals(estado))
                    .OrderByDescending(p => p.FechaCompra);
        }

        public Bono Get(int id)
        {
            return bonoRepo.Get(id);
        }

        public IEnumerable GetBonosCliente(int clientId)
        {
            return bonoRepo.Get()
                .Where(x => x.ClienteId == clientId)
                .OrderByDescending(x => x.FechaCompra);
        }

        public void PagarBono(int bonoId)
        {
            //get bono
            var bono = bonoRepo.Get(bonoId);

            if (bono == null)
                throw new KeyNotFoundException();

            SetBonoPagado(bono);
            bonoRepo.Update(bono);
            CrearHistorialBonoPagado(bono.Id);

            bonoRepo.Save();
        }

        private void SetBonoPagado(Bono bono)
        {
            var estadosRepo = new Repository<EstadoBono>(_db);
            var estadoCobrado = estadosRepo.Get().FirstOrDefault(x => x.Descripcion.Equals(EstadosBonos.Cobrado));

            if (estadoCobrado != null)
                bono.EstadoBonoId = estadoCobrado.Id;
        }

        private void CrearHistorialBonoPagado(int bonoId)
        {
            var historialRepo = new Repository<HistorialBono>(_db);

            HistorialBono hist = new HistorialBono
            {
                BonoId = bonoId,
                EstadoBonoId = getEstadoId(EstadosBonos.Cobrado),
                FechaEntradaEstado = DateTime.Now
            };

            historialRepo.Insert(hist);
            historialRepo.Save();
        }

        private int getEstadoId(EstadosBonos estado)
        {
            var estadosRepo = new Repository<EstadoBono>(_db);

            int id = 0;
            var est = estadosRepo.Get().FirstOrDefault(x => x.Descripcion.Equals(estado));
            if (est != null)
                id = est.Id;

            return id;
        }
    }
}
