using EMSApi.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApi.Data
{
    public class EMSDBContext : IdentityDbContext<APIUser>
    {
        public EMSDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
