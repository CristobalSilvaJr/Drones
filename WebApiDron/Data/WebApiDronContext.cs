using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiDron.Models;

namespace WebApiDron.Data
{
    public class WebApiDronContext : DbContext
    {
        public WebApiDronContext (DbContextOptions<WebApiDronContext> options)
            : base(options)
        {
        }

        public DbSet<WebApiDron.Models.MedicamentoModels> MedicamentoModels { get; set; }

        public DbSet<WebApiDron.Models.DronModels> DronModels { get; set; }
    }
}
