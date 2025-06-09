using CryptoManager.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; //instalo entity desde adm paquetes del Nuggets

namespace CryptoManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}




    
