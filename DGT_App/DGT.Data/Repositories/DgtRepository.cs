using System;
using System.Collections.Generic;
using System.Text;
using DGT.Data.Domain.Entities;

namespace DGT.Data.Infrastructure.Repositories
{
    public class DgtRepository : IDgtRepository
    {
        private DgtContext _context;

        public DgtRepository(DgtContext context)
        {
            _context = context ?? throw new NullReferenceException();
        }

        public void AddConductor(Conductor vehiculo)
        {
            throw new NotImplementedException();
        }

        public void AddInfraccion(Infraccion vehiculo)
        {
            throw new NotImplementedException();
        }

        public void AddTipoInfraccion(TipoInfraccion vehiculo)
        {
            throw new NotImplementedException();
        }

        public void AddVehiculo(Vehiculo vehiculo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoInfraccion> GetInfraccionesComunes(int topMax)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Infraccion> GetInfraccionesConductor(string dni)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Conductor> GeTopNConductores(int topMax)
        {
            throw new NotImplementedException();
        }
    }
}
