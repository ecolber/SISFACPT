using System.Linq.Expressions;

namespace CLNFactPT.Datos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Obtener(int id);

        Task<IEnumerable<T>> ObtenerTodos();
        Task Agregar(T entidad);
        void Eliminar(T entidad);
    }
}
