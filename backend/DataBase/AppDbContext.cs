using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

    }


}