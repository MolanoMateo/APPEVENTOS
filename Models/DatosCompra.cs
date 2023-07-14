using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPEVENTOS.Models
{
    public class DatosCompra
    {
        public int Id { get; set; }
        public int cantidad { get; set; }
        public String fechaCompra { get; set; }
        public String titulo { get; set; }
        public String descripcion { get; set; }
        public float costo { get; set; }
        public String ubicacion { get; set; }
        public String fechaEvento { get; set; }
    }
}
