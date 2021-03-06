﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using NTAccounting.Models;

namespace NTAccounting.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserGroupApplicationUser>()
                .HasKey(k => new { k.UserGroupID, k.ApplicationUserID });

            builder.Entity<UserGroupApplicationUser>()
                .HasOne(m => m.UserGroup)
                .WithMany(r => r.UserGroupApplicationUser)
                .HasForeignKey(fk => fk.UserGroupID);

            builder.Entity<UserGroupApplicationUser>()
                .HasOne(m => m.ApplicationUser)
                .WithMany(r => r.UserGroupApplicationUser)
                .HasForeignKey(fk => fk.ApplicationUserID);

            builder.Entity<Transaction>()
                .HasOne(f => f.FinancialAccount)
                .WithMany(t => t.Transactions)
                .HasForeignKey(fk => fk.FinancialAccountID);

            builder.Entity<Transaction>()
                .HasOne(f => f.TargetFinancialAccount)
                .WithMany(t => t.TransferTransactions)
                .HasForeignKey(tfk => tfk.TargetFinancialAccountID)
                .IsRequired(false);
                
        }
        public DbSet<FinancialAccount> FinancialAccount { get; set; }
        public DbSet<FinancialAccountType> FinancialAccountType { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserGroupApplicationUser> UserGroupApplicationUser { get; set; }
        public DbSet<MainTransactionCategory> MainTransactionCategory { get; set; }
        public DbSet<SubTransactionCategory> SubTransactionCategory { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
