using AccountantWPF.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Data
{
    public class AccountantDbContext : DbContext
    {
        public AccountantDbContext(DbContextOptions<AccountantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<CashPos> CashPosIncomes { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AccountantWPF.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
