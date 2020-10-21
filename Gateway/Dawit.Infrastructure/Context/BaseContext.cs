using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawit.Domain.Model;

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
            });
            //TODO: nao testei
            /*modelBuilder.Entity<AppUser>().Property(user => user.CreationDate)
                .HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();*/

        }
    }
}
