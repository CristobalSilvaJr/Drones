using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDron.Models
{
    public class MedicamentoModels
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Peso { get; set; }
        public string Codigo { get; set; }
        public string Imagen { get; set; }
    }
}
