using Brokerage.Service.Infrastructure.Entities;
using Brokerage.Service.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Service.Infrastructure
{
    public class BrokerageContext : DbContext
    {
        public DbSet<ForeignCurrencyTransactions> ForeignCurrencyTransactions { get; set; }

        public BrokerageContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ForeignCurrencyTransactionsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
