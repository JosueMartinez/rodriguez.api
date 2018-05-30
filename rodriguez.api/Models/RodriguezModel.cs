namespace rodriguez.api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.ModelConfiguration.Conventions;

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


        public virtual DbSet<Bono> Bonos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<EstadoBono> Estados { get; set; }
        public virtual DbSet<HistorialBono> Historial { get; set; }
        public virtual DbSet<ListaCompra> Listas { get; set; }
        public virtual DbSet<ListaProducto> ListasProductos { get; set; }
        public virtual DbSet<Medida> Medidas { get; set; }
        public virtual DbSet<Moneda> Monedas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Tasa> Tasas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Bono>()
                .Property(e => e.NombreDestino)
                .IsUnicode(false);

            modelBuilder.Entity<Bono>()
                .Property(e => e.ApellidoDestino)
                .IsUnicode(false);

            modelBuilder.Entity<Bono>()
                .Property(e => e.CedulaDestino)
                .IsUnicode(false);

            modelBuilder.Entity<Bono>()
                .HasMany(e => e.Historial)
                .WithRequired(e => e.Bono)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Productos)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);
            //modelBuilder.Entity<cliente>()
            //    .Property(e => e.nombres)
            //    .IsUnicode(false);

            //modelBuilder.Entity<cliente>()
            //    .Property(e => e.apellidos)
            //    .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Bonos)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Listas)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstadoBono>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<EstadoBono>()
                .HasMany(e => e.Bonos)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstadoBono>()
                .HasMany(e => e.Historial)
                .WithRequired(e => e.Estadobono)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ListaCompra>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ListaCompra>()
                .HasMany(e => e.Productos)
                .WithRequired(e => e.Lista)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medida>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Medida>()
                .Property(e => e.Simbolo)
                .IsUnicode(false);

            modelBuilder.Entity<Medida>()
                .HasMany(e => e.Productos)
                .WithRequired(e => e.Medida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Moneda>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Moneda>()
                .Property(e => e.Simbolo)
                .IsUnicode(false);

            //modelBuilder.Entity<moneda>()
            //    .HasMany(e => e.bonoes)
            //    .WithRequired(e => e.moneda)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Moneda>()
                .HasMany(e => e.Tasas)
                .WithRequired(e => e.Moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.nombres)
            //    .IsUnicode(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.apellidos)
            //    .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Contrasena)
                .IsUnicode(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}
