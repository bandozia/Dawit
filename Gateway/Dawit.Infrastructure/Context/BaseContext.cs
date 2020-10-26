using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawit.Domain.Model.Auth;
using Dawit.Domain.Model.Neural;

namespace Dawit.Infrastructure.Context
{
    public class BaseContext : DbContext
    {        
        public BaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasPostgresExtension("uuid-ossp");
            
            modelBuilder.Entity<AppUser>(user =>
            {
                user.HasKey(u => u.Id);
                user.HasIndex(u => u.Id).IsUnique();
                user.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<NeuralJob>(njob =>
            {
                njob.HasKey(n => n.Id);
                njob.HasIndex(n => n.Id).IsUnique();
                njob.HasMany(n => n.Metrics).WithOne(m => m.NeuralJob);
            });
        }
    }
}
