using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class UtentiContext : DbContext
{
    public UtentiContext()
    {
    }

    public UtentiContext(DbContextOptions<UtentiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DatiUtente> DatiUtentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=9DH0LX3-A081\\SQLEXPRESS;Database=Utenti;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatiUtente>(entity =>
        {
            entity.HasKey(e => e.CodiceFiscale);

            entity.ToTable("DatiUtente");

            entity.Property(e => e.CodiceFiscale).HasMaxLength(50);
            entity.Property(e => e.Cognome).HasMaxLength(50);
            entity.Property(e => e.Comune).HasMaxLength(50);
            entity.Property(e => e.DataDiNascita).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Sesso).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
