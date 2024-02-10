using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MailsWepApi.DataBase
{
    public partial class DBContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DBContext(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public virtual DbSet<Mail> Mail { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DESKTOP-23SNJ6U\\SQLEXPRESS;Database=MailsWebApi;Trusted_Connection=True;");
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>(entity => 
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Body).IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FailedMessage).IsUnicode(false);

                entity.Property(e => e.Recipients).IsUnicode(false);

                entity.Property(e => e.Subject).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
