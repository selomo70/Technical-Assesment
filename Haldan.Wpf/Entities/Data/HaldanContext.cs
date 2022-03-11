using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Data
{
    public class HaldanContext:DbContext
    {
        public HaldanContext()
     : base("HaldanContext")
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Communication> Communications { get; set; }
    }
}
