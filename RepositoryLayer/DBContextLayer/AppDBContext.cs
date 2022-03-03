using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DBContextLayer
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions con) : base (con)
        {

        }
        public DbSet<User> tblUsers { get; set; }
    }
}
