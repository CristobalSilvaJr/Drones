using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDron.Models
{
    public class DronMedicamento
    {
        public int Id { get; set; }
        public string CodigoMedicamento { get; set; }
        [Key]
        public string CodigoDron { get; set; }
    }
}
