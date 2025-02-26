using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WPF_Proj.Models;
using Microsoft.EntityFrameworkCore;
using System.IO.Packaging;
using WPF_Proj.Service;
namespace WPF_Proj.BD_integration
{


    public class GraphDbContext : DbContext
    {
        public DbSet<GraphData> Graphs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=graphs.db");
        }
    }
}
    

