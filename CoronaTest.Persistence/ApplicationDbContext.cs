using CoronaTest.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CoronaTest.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<TestCenter> TestCenters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            Debug.WriteLine(configuration);

            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
        }
    }
}
