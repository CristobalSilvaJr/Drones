﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDron.Models
{
    public class MedicamentoModels
    {
        [Key]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Peso { get; set; }
        public string Imagen { get; set; }
    }
}
