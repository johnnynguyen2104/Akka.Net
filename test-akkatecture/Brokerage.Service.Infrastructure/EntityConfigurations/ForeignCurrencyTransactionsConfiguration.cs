using Brokerage.Service.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brokerage.Service.Infrastructure.EntityConfigurations
{
    public class ForeignCurrencyTransactionsConfiguration : IEntityTypeConfiguration<ForeignCurrencyTransactions>
    {
        public void Configure(EntityTypeBuilder<ForeignCurrencyTransactions> builder)
        {
            builder.HasKey(a => a.TransactionId);

            builder.Property(a => a.CurrencyPairType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(a => a.PaymentProviderType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(a => a.SettlementStatusType)
               .HasConversion<int>()
               .IsRequired();

            builder.Property(a => a.Reason)
               .HasConversion<int>();
        }
    }
}
