﻿using AccountantWPF.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Data
{
    public class AccountantDbContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<CashPosIncome> CashPosIncomes { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AccountantWPF.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
