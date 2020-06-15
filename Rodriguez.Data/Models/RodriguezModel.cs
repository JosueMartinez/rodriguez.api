namespace Rodriguez.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class RodriguezModel : IdentityDbContext<IdentityUser>
    {
        // Your context has been configured to use a 'RodriguezModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Rodriguez.Data.Models.RodriguezModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RodriguezModel' 
        // connection string in the application configuration file.
        public RodriguezModel()
            : base("name=RodriguezModel")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Bono> Bonos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<EstadoBono> EstadosBonos { get; set; }
        public virtual DbSet<HistorialBono> HistorialBonos { get; set; }
        public virtual DbSet<Moneda> Monedas { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<TasaMoneda> TasasMonedas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}