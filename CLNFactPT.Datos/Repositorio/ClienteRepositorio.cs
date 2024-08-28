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
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly AppDbContext _db;

        public ClienteRepositorio(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Cliente cliente)
        {
            var clienteDb = _db.Clientes.FirstOrDefault(x => x.Id == cliente.Id);

            if (clienteDb != null)
            {
                clienteDb.Codigo = cliente.Codigo;
                clienteDb.Nombre = cliente.Nombre;
                clienteDb.Apellido = cliente.Apellido;                
                _db.SaveChanges();
            }
        }

        public async Task<Cliente> ObtenerPorCodigo(string codigo)
        {
            return await _db.Set<Cliente>().FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<Cliente> ObtenerPorNombre(string nombre)
        {
            return await _db.Set<Cliente>().FirstOrDefaultAsync(c => c.Nombre == nombre);
        }
    }
}
