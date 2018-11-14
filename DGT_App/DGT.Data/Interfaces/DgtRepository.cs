using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DGT.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DGT.Data.Infrastructure.Interfaces
{
    public class DgtRepository : IDgtRepository
    {
        private DgtContext _context;

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public DgtRepository(DgtContext context)
        {
            _context = context;
        }

        public void AddConductor(Conductor conductor)
        {
            _context.Conductores.Add(conductor);
        }

        public void AddTipoInfraccion(TipoInfraccion tipoInfraccion)
        {
            _context.TiposDeInfracciones.Add(tipoInfraccion);
        }

        public void AddInfraccion(Infraccion infraccion)
        {
            _context.Infracciones.Add(infraccion);
        }

        public void AddVehiculo(Vehiculo vehiculo, string dniConductor)
        {
            var conductor = GetConductor(dniConductor);
            if (conductor != null)
            {
                vehiculo.ConductoresHabituales.Add(new ConductorVehiculo()
                                                {
                                                    Conductor = conductor,
                                                    Vehiculo = vehiculo,
                                                });
            }

            _context.Vehiculos.Add(vehiculo);
        }

        public Conductor GetConductor(string dni)
        {
            return _context.Conductores.Where(c => String.Compare(c.DNI, dni, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefault();
        }

        public Vehiculo GetVehiculo(string matricula)
        {
            return _context.Vehiculos.Include(v => v.ConductoresHabituales).ThenInclude(ch => ch.Conductor).Where(c => String.Compare(c.Matricula, matricula, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefault();
        }
        public TipoInfraccion GetTipoInfraccion(string idTipoInfraccion)
        {
            return _context.TiposDeInfracciones.Where(c => String.Compare(c.Id, idTipoInfraccion, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefault();
        }

        public List<Vehiculo> GetVehiculosParaConductor(string dniConductor)
        {
            return _context.Vehiculos.Where(v => v.ConductoresHabituales.Any(ch => String.Compare(ch.Conductor.DNI, dniConductor, StringComparison.CurrentCultureIgnoreCase) == 0)).ToList();
        }

        public List<Infraccion> GetInfraccionesConductor(string dniConductor)
        {
            return _context.Infracciones.Where(i => String.Compare(i.Conductor.DNI, dniConductor, StringComparison.CurrentCultureIgnoreCase) == 0).ToList();
        }

        public List<TipoInfraccion> GetTiposInfraccionesComunes(int num)
        {
            var subQuery = from i in _context.Infracciones
                        group i by i.TipoInfraccion into g
                        select new {
                                        NumeroInfracciones = g.Count(),
                                        TipoInfraccion = g.Key
                                   };

            var query = subQuery.OrderBy(g => g.NumeroInfracciones).ToList();

            return query.Take(num).Select(g => g.TipoInfraccion).ToList();
        }
        public List<Conductor> GetMejoresConductores(int topN)
        {
            return _context.Conductores.OrderBy(c => c.Puntos).Take(topN).ToList();
        }

        public void UpdateConductor(Conductor conductor)
        {
            _context.Conductores.Update(conductor);
        }
    }
}
