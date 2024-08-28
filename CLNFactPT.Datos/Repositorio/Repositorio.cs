using CLNFactPT.Datos.Data;
using CLNFactPT.Datos.Repositorio.IRepositorio;
using CLNFactPT.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CLNFactPT.Datos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(AppDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await dbSet.ToListAsync();
        }
        public void Eliminar(T entidad)
        {
            dbSet.Remove(entidad);
        }
    }
}
