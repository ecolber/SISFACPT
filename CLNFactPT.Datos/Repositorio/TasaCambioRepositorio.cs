using CLNFactPT.Datos.Data;
using CLNFactPT.Datos.Repositorio.IRepositorio;
using CLNFactPT.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.Repositorio
{
    public class TasaCambioRepositorio : Repositorio<TasaCambio>, ITasaCambioRepositorio
    {
        private readonly AppDbContext _db;

        public TasaCambioRepositorio(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(TasaCambio tasaCambio)
        {
            var tcDb = _db.TasaCambios.FirstOrDefault(x => x.Id == tasaCambio.Id);

            if (tcDb != null)
            {
                tcDb.Fecha = tasaCambio.Fecha;
                tcDb.TasaDeCambio = tasaCambio.TasaDeCambio;               
                _db.SaveChanges();
            }
        }

        public async Task<TasaCambio> ObtenerPorFecha(string fecha)
        {
            return await _db.Set<TasaCambio>().FirstOrDefaultAsync(tc => tc.Fecha == Convert.ToDateTime(fecha));
        }

        public async Task<List<TasaCambio>> ObtenerPorMes(int mes)
        {
            return await _db.Set<TasaCambio>().Where(tc => tc.Fecha.Month == mes).ToListAsync();
        }
    }
}
