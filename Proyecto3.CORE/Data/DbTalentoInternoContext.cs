using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto3.CORE.Data;

public partial class DbTalentoInternoContext : DbContext
{
    public DbTalentoInternoContext()
    {
    }

    public DbTalentoInternoContext(DbContextOptions<DbTalentoInternoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Certificaciones> Certificaciones { get; set; }

    public virtual DbSet<Colaboradores> Colaboradores { get; set; }

    public virtual DbSet<HabilidadesColaborador> HabilidadesColaborador { get; set; }

    public virtual DbSet<PerfilesVacante> PerfilesVacante { get; set; }

    public virtual DbSet<ReporteBrechas> ReporteBrechas { get; set; }

    public virtual DbSet<RequisitosVacante> RequisitosVacante { get; set; }

    public virtual DbSet<Skills> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        // Only configure a fallback connection string when the options haven't been configured by DI
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, move it out of source code or use configuration.
            optionsBuilder.UseSqlServer("Server=LAPTOP-5QVS7TDV;Database=DB_TalentoInterno;Integrated Security=True;TrustServerCertificate=True");
        }
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Certificaciones>(entity =>
        {
            entity.HasKey(e => e.CertificacionId).HasName("PK__Certific__398F23E446D0A6C3");

            entity.Property(e => e.CertificacionId).HasColumnName("CertificacionID");
            entity.Property(e => e.ColaboradorId).HasColumnName("ColaboradorID");
            entity.Property(e => e.Institucion).HasMaxLength(100);
            entity.Property(e => e.NombreCertificacion).HasMaxLength(200);

            entity.HasOne(d => d.Colaborador).WithMany(p => p.Certificaciones)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Certifica__Colab__571DF1D5");
        });

        modelBuilder.Entity<Colaboradores>(entity =>
        {
            entity.HasKey(e => e.ColaboradorId).HasName("PK__Colabora__28AA72C1D33C523D");

            entity.Property(e => e.ColaboradorId).HasColumnName("ColaboradorID");
            entity.Property(e => e.CuentaProyecto).HasMaxLength(100);
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.RolActual).HasMaxLength(100);
        });

        modelBuilder.Entity<HabilidadesColaborador>(entity =>
        {
            entity.HasKey(e => e.HabilidadColaboradorId).HasName("PK__Habilida__23272F79D0BC8C92");

            entity.HasIndex(e => new { e.ColaboradorId, e.SkillId }, "UQ_Habilidad_Colaborador").IsUnique();

            entity.Property(e => e.HabilidadColaboradorId).HasColumnName("HabilidadColaboradorID");
            entity.Property(e => e.ColaboradorId).HasColumnName("ColaboradorID");
            entity.Property(e => e.NivelDominio).HasMaxLength(50);
            entity.Property(e => e.SkillId).HasColumnName("SkillID");

            entity.HasOne(d => d.Colaborador).WithMany(p => p.HabilidadesColaborador)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habilidad__Colab__4D94879B");

            entity.HasOne(d => d.Skill).WithMany(p => p.HabilidadesColaborador)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habilidad__Skill__4E88ABD4");
        });

        modelBuilder.Entity<PerfilesVacante>(entity =>
        {
            entity.HasKey(e => e.VacanteId).HasName("PK__Perfiles__4CFFE249455FA078");

            entity.Property(e => e.VacanteId).HasColumnName("VacanteID");
            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.NivelDeseado).HasMaxLength(50);
            entity.Property(e => e.NombrePerfil).HasMaxLength(100);
        });

        modelBuilder.Entity<ReporteBrechas>(entity =>
        {
            entity.HasKey(e => e.ReporteId).HasName("PK__ReporteB__0B29EA4E07E98659");

            entity.Property(e => e.ReporteId).HasColumnName("ReporteID");
            entity.Property(e => e.Area).HasMaxLength(100);
            entity.Property(e => e.BrechaPorcentaje).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaReporte)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SkillId).HasColumnName("SkillID");

            entity.HasOne(d => d.Skill).WithMany(p => p.ReporteBrechas)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReporteBr__Skill__5AEE82B9");
        });

        modelBuilder.Entity<RequisitosVacante>(entity =>
        {
            entity.HasKey(e => e.RequisitoId).HasName("PK__Requisit__372DF81A76ECE4AF");

            entity.HasIndex(e => new { e.VacanteId, e.SkillId }, "UQ_Requisito_Vacante").IsUnique();

            entity.Property(e => e.RequisitoId).HasColumnName("RequisitoID");
            entity.Property(e => e.NivelMinimoRequerido).HasMaxLength(50);
            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.VacanteId).HasColumnName("VacanteID");

            entity.HasOne(d => d.Skill).WithMany(p => p.RequisitosVacante)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisito__Skill__5441852A");

            entity.HasOne(d => d.Vacante).WithMany(p => p.RequisitosVacante)
                .HasForeignKey(d => d.VacanteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisito__Vacan__534D60F1");
        });

        modelBuilder.Entity<Skills>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skills__DFA091E7527E2541");

            entity.HasIndex(e => e.NombreSkill, "UQ__Skills__B7AB7AE309556974").IsUnique();

            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.NombreSkill).HasMaxLength(100);
            entity.Property(e => e.TipoSkill).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
