using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Hosting;
using Vuelos.Models.Entity;

namespace Vuelos.DAL.DataContext;

public partial class VuelosContext : DbContext
{
    public VuelosContext()
    {
    }

    public VuelosContext(DbContextOptions<VuelosContext> options)
        : base(options)
    {
    }

    public DbSet<Transport> Transports { get; set; }
    public DbSet<Journey> Journeys { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<JourneyFlight> JourneyFlights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var hostEnvironment = new HostBuilder().Build().Services.GetService(typeof(IHostEnvironment)) as IHostEnvironment;
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Vuelos.DAL");
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("cadenaSQL");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

      

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<JourneyFlight>()
       .HasKey(jf => new { jf.JourneyId, jf.FlightId, jf.NRuta, jf.Type });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
