using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Model
{
    public class PaymentDetailContext : DbContext
    {
        public PaymentDetailContext(DbContextOptions<PaymentDetailContext> option):base(option)
        {   }

        public DbSet<PaymentDetails> PaymentDetail { get; set; }
    }
}