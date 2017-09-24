using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace World.Models
{
    public class WorldContext : DbContext
    {
        //use this approach when setting up in startup.cs -> Configuration
        public WorldContext(DbContextOptions options): base(options)
        {
        }

        //use this approach when setting up in place by overriding OnConfiguring method
        //private IConfigurationRoot _config;
        //public WorldContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        //{
        //    _config = config;
        //}

        public DbSet<Trip> Trips { get; set; }        
        public DbSet<Stop> Stops { get; set; }

        // alternative approach to injection
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnectionString"));
        //}
    }
}
