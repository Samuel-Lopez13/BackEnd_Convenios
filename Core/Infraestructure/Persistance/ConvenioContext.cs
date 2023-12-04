using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infraestructure.Persistance
{
    public class ConvenioContext : DbContext{

        public ConvenioContext(DbContextOptions<ConvenioContext> options): base(options) { }

        public DbSet<Contrato> Contratos { get; set; } = null!;
        public DbSet<Alerta> Alertas { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<Intercambio> Intercambios { get; set; } = null!;
        public DbSet<Institucion> Instituciones { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.HasKey(e => e.Contrato_Id);

                entity.Property(e => e.FechaCreacion).IsRequired();

                entity.Property(e => e.Descripcion).IsRequired();

                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.Property(e => e.Status).IsRequired();
                
                entity.Property(e => e.File).IsRequired();

                entity.HasOne(d => d.Instituciones)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.Institucion_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agreement_ibfk_1");
            });

            modelBuilder.Entity<Alerta>(entity =>
            {
                entity.HasKey(e => e.Alerta_Id);
                
                entity.Property(e => e.IsAdmin).IsRequired();
                
                entity.Property(e => e.IsInstitucion).IsRequired();

                entity.HasOne(d => d.Contratos)
                    .WithMany(p => p.Alerts)
                    .HasForeignKey(d => d.Contrato_Id)
                    .HasConstraintName("Alert_ibfk_1");
            });
            
            modelBuilder.Entity<Intercambio>(entity =>
            {
                entity.HasKey(e => e.Intercambio_Id);

                entity.HasOne(d => d.Contratos)
                    .WithMany(p => p.Intercambios)
                    .HasForeignKey(d => d.Contrato_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Intercambio_ibfk_1");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.Chat_Id);

                entity.Property(e => e.Fecha).IsRequired();

                entity.Property(e => e.Mensaje).IsRequired();

                entity.HasOne(d => d.Contratos)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.Contrato_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Chat_ibfk_1");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.Usuario_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Chat_ibfk_2");
            });

            modelBuilder.Entity<Institucion>(entity =>
            {
                entity.HasKey(e => e.Institucion_Id);

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.Ciudad).HasMaxLength(50);

                entity.Property(e => e.Pais).HasMaxLength(50);

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.Property(e => e.Estado).HasMaxLength(50);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Log_Id);

                entity.Property(e => e.Action).IsRequired();

                entity.Property(e => e.Fecha).IsRequired();

                entity.HasOne(d => d.Contratos)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.Contrato_Id)
                    .HasConstraintName("Log_ibfk_1");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.Usuario_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Log_ibfk_2");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Rol_Id);

                entity.Property(e => e.Descripcion).IsRequired();

                entity.Property(e => e.Nombre).IsRequired();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Usuario_Id);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired();
                
                entity.Property(e => e.Password).HasMaxLength(50).IsRequired();

                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();

                entity.HasOne(d => d.Instituciones)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Institucion_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_institution_FK");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Rol_Id)
                    .HasConstraintName("User_ibfk_1");
            });

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
