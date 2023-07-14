using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPEVENTOS.Models
{
    internal class ResultadoApi
    {
        public string httpResponseCode { get; set; }

        public List<Usuario> LUsuario { get; set; }

        public Usuario NUsuario { get; set; }
        public List<Evento> LEvento { get; set; }

        public Evento NEvento { get; set; }
        public List<Venta> LVenta { get; set; }

        public Venta NVenta { get; set; }
    }
}
