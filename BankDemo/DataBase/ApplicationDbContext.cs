using BankDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BankDemo.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<TransactionModel> TransactionModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1251");

            modelBuilder.Entity<TransactionModel>(entity =>
            {
                entity.HasKey(e => e.Transactions_Id)
                    .HasName("Transactions_pkey");

                entity.ToTable("transactions");

                entity.Property(e => e.Transactions_Id)
                    .HasColumnName("transactions_id");
               

                entity.Property(e => e.Credit)
                    .HasColumnType("money")
                    .HasColumnName("credit");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Debit)
                    .HasColumnType("money")
                    .HasColumnName("debit");

                entity.Property(e => e.Receiver)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("receiver");

                entity.Property(e => e.Sender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sender");
            });

            modelBuilder.Entity<TransactionModel>(entity =>
            {

                base.OnModelCreating(modelBuilder);
            });
        }
    }
}
