using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDron.Models
{
    public class DronModels
    {
        //NumeroSerie= ID
        [Key]
        public string NumeroSerie { get; set; } 
        public string Modelo { get; set; }
        public int PesoLimite { get; set; }
        //TODO:Capacidad de la bateria: Este seria la carga del dron.
        public decimal CapacidadBateria { get; set; }
        public string Estado { get; set; }

    }
}
