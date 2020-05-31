using BlazorBarcode.gRPC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBarcode.gRPC.Repository
{
    public class MyContext : DbContext
    {
        private readonly DbSettings _dbSettings;
        public MyContext(DbContextOptions<MyContext> options, IOptions<DbSettings> dbSettings) : base(options)
        {
            _dbSettings = dbSettings.Value;
        }
        public MyContext()
        {

        }

        public DbSet<BarcodeProduct> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarcodeProduct>(entity =>
            {
                entity.HasKey(x => x.BarcodeNo);
            });
            //base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "User ID=postgres;Password=sa;Host=localhost;Port=5432;Database=BlazorBarcode;Pooling=true;Integrated Security=true;";
            optionsBuilder.UseNpgsql(conn);
            //base.OnConfiguring(optionsBuilder);

        }
    }
}
