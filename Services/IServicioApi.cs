using APPEVENTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPEVENTOS.Services
{
    public interface IServicioApi
    {
        Task<List<Evento>> ListarEventos();
        Task<List<DatosCompra>> ObtenerCompras(string cedula);
        Task<DatosCompra> ObtenerVenta(int id);
        Task<Usuario> LoginUsuario(Usuario usuario);
        Task<string> GuardarUsuario(Usuario cliente);
        Task<string> GuardarVenta(Venta venta);
        Task<string> EditarUsuario(string cedula, Usuario cliente);
        Task<string> EliminarUsuario(string cedula);
    }
}
