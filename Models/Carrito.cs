using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace APPEVENTOS.Models
{
    [Table("carrito")]
    public class Carrito
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int idEvento { get; set; }
        public int cantidad { get; set; }
        public String fechaCompra { get; set; }
        public String titulo { get; set; }
        public String descripcion { get; set; }
        public float costo { get; set; }
        public float total { get; set; }
    }
}