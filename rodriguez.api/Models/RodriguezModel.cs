namespace rodriguez.api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RodriguezModel : DbContext
    {
        public RodriguezModel()
            : base("name=RodriguezModel")
        {
        }

        public virtual DbSet<bono> bono { get; set; }
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<estado_bono> estado_bono { get; set; }
        public virtual DbSet<historial_bono> historial_bono { get; set; }
        public virtual DbSet<lista_compra> lista_compra { get; set; }
        public virtual DbSet<lista_compra_producto> lista_compra_producto { get; set; }
        public virtual DbSet<medida> medida { get; set; }
        public virtual DbSet<moneda> moneda { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<rol> rol { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<bono>()
                .Property(e => e.nombre_destino)
                .IsUnicode(false);

            modelBuilder.Entity<bono>()
                .Property(e => e.apellido_destino)
                .IsUnicode(false);

            modelBuilder.Entity<bono>()
                .Property(e => e.cedula_destino)
                .IsUnicode(false);

            modelBuilder.Entity<bono>()
                .HasMany(e => e.historial_bono)
                .WithRequired(e => e.bono1)
                .HasForeignKey(e => e.bono)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<categoria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.cedula)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.bono)
                .WithRequired(e => e.cliente)
                .HasForeignKey(e => e.remitente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.lista_compra)
                .WithRequired(e => e.cliente)
                .HasForeignKey(e => e.creador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estado_bono>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<estado_bono>()
                .HasMany(e => e.bono)
                .WithRequired(e => e.estado_bono)
                .HasForeignKey(e => e.estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estado_bono>()
                .HasMany(e => e.historial_bono)
                .WithRequired(e => e.estado_bono)
                .HasForeignKey(e => e.estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lista_compra>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<lista_compra>()
                .HasMany(e => e.lista_compra_producto)
                .WithRequired(e => e.lista_compra)
                .HasForeignKey(e => e.lista)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<medida>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<medida>()
                .Property(e => e.simbolo)
                .IsUnicode(false);

            modelBuilder.Entity<moneda>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<moneda>()
                .Property(e => e.simbolo)
                .IsUnicode(false);

            modelBuilder.Entity<moneda>()
                .HasMany(e => e.bono)
                .WithRequired(e => e.moneda1)
                .HasForeignKey(e => e.moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.medida)
                .IsUnicode(false);

            modelBuilder.Entity<rol>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<rol>()
                .HasMany(e => e.usuario)
                .WithOptional(e => e.rol1)
                .HasForeignKey(e => e.rol);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombre_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.contrasena)
                .IsUnicode(false);
        }
    }
}
