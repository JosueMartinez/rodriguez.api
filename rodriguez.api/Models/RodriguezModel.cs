namespace rodriguez.api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    //public partial class RodriguezModel : DbContext
    public partial class RodriguezModel : IdentityDbContext<IdentityUser>
    {
        public RodriguezModel()
            : base("name=RodriguezModel")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        //static RodriguezModel()
        //{
        //    DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        //}
        
        
        public virtual DbSet<bono> bonos { get; set; }
        public virtual DbSet<categoria> categorias { get; set; }
        public virtual DbSet<cliente> clientes { get; set; }
        public virtual DbSet<estadobono> estadobonos { get; set; }
        public virtual DbSet<historialbono> historialbonos { get; set; }
        public virtual DbSet<listacompra> listacompras { get; set; }
        public virtual DbSet<listacompraproducto> listacompraproductos { get; set; }
        public virtual DbSet<medida> medidas { get; set; }
        public virtual DbSet<moneda> monedas { get; set; }
        public virtual DbSet<producto> productos { get; set; }
        public virtual DbSet<rol> rols { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        public virtual DbSet<tasamoneda> tasasmonedas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<bono>()
                .Property(e => e.nombreDestino)
                .IsUnicode(false);

            modelBuilder.Entity<bono>()
                .Property(e => e.apellidoDestino)
                .IsUnicode(false);

            modelBuilder.Entity<bono>()
                .Property(e => e.cedulaDestino)
                .IsUnicode(false);

            modelBuilder.Entity<bono>()
                .HasMany(e => e.historialbonoes)
                .WithRequired(e => e.bono)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<categoria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<categoria>()
                .HasMany(e => e.productos)
                .WithRequired(e => e.categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.nombreCompleto)
                .IsUnicode(false);
            //modelBuilder.Entity<cliente>()
            //    .Property(e => e.nombres)
            //    .IsUnicode(false);

            //modelBuilder.Entity<cliente>()
            //    .Property(e => e.apellidos)
            //    .IsUnicode(false);

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
                .HasMany(e => e.bonoes)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.listacompras)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estadobono>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<estadobono>()
                .HasMany(e => e.bonoes)
                .WithRequired(e => e.estadobono)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estadobono>()
                .HasMany(e => e.historialbonoes)
                .WithRequired(e => e.estadobono)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<listacompra>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<listacompra>()
                .HasMany(e => e.productosLista)
                .WithRequired(e => e.listacompra)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<medida>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<medida>()
                .Property(e => e.simbolo)
                .IsUnicode(false);

            modelBuilder.Entity<medida>()
                .HasMany(e => e.productos)
                .WithRequired(e => e.medida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<moneda>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<moneda>()
                .Property(e => e.simbolo)
                .IsUnicode(false);

            //modelBuilder.Entity<moneda>()
            //    .HasMany(e => e.bonoes)
            //    .WithRequired(e => e.moneda)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<moneda>()
                .HasMany(e => e.tasas)
                .WithRequired(e => e.moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<rol>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<rol>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombreCompleto)
                .IsUnicode(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.nombres)
            //    .IsUnicode(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.apellidos)
            //    .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.contrasena)
                .IsUnicode(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}
