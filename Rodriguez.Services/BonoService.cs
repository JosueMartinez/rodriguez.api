using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rodriguez.Common;
using Rodriguez.Data.Models;
using Rodriguez.Data.Utils;
using Rodriguez.Repo;
using Rodriguez.Services.Interfaces;

namespace Rodriguez.Services
{
    public class BonoService : IBonoService
    {
        private readonly RodriguezModel _db;
        //private readonly Repository<Bono> bonoRepo;
        private readonly BonoRepository bonoRepo;

        public BonoService(RodriguezModel db)
        {
            _db = db;
            bonoRepo = new BonoRepository();
        }

        public Bono AddBono(Bono bono)
        {
                InflateNewBono(bono);
                bonoRepo.Insert(bono);
                bonoRepo.Save();

                return bono;
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
            return bonoRepo.Get(estado);
        }

        public Bono Get(int id)
        {
            return bonoRepo.Get(id);
        }

        public IEnumerable GetBonosCliente(int clientId)
        {
            return bonoRepo.GetClient(clientId);
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
            var estadoCobradoId = getEstadoId(EstadosBonos.Cobrado);  //estadosRepo.Get().FirstOrDefault(x => x.Descripcion.Equals(EstadosBonos.Cobrado));

            //if (estadoCobradoId != null)
                bono.EstadoBonoId = estadoCobradoId;
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
            var est = estadosRepo.Get().FirstOrDefault(x => x.Descripcion.Equals(estado.GetDescription()));
            if (est != null)
                id = est.Id;

            return id;
        }
    }
}
