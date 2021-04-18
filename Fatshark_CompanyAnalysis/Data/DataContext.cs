using Fatshark_CompanyAnalysis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.Data
{
    class DataContext : DbContext
    {
        public DbSet<CompanySet> Datasets { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Path.Combine(Environment.CurrentDirectory, @"Files\", "\testdatabase.db"));
        }
    }
}
