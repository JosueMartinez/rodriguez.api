namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Double(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        TasaId = c.Int(nullable: false),
                        PaypalId = c.String(unicode: false),
                        NombreDestino = c.String(nullable: false, maxLength: 50, unicode: false),
                        ApellidoDestino = c.String(nullable: false, maxLength: 50, unicode: false),
                        CedulaDestino = c.String(nullable: false, maxLength: 11, unicode: false),
                        TelefonoDestino = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        EstadoBonoId = c.Int(nullable: false),
                        FechaCompra = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.EstadoBono", t => t.EstadoBonoId)
                .ForeignKey("dbo.Tasa", t => t.TasaId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.TasaId)
                .Index(t => t.EstadoBonoId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        NombreCompleto = c.String(nullable: false, maxLength: 200, unicode: false),
                        Cedula = c.String(nullable: false, maxLength: 11, unicode: false),
                        Celular = c.String(maxLength: 10, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ClienteId)
                .Index(t => t.Usuario, unique: true);
            
            CreateTable(
                "dbo.ListaCompra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50, unicode: false),
                        ClienteId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false, precision: 0),
                        FechaUltimaModificacion = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.ListaProducto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListaCompraId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producto", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.ListaCompra", t => t.ListaCompraId)
                .Index(t => t.ListaCompraId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30, unicode: false),
                        MedidaId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Medida", t => t.MedidaId)
                .Index(t => t.MedidaId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medida",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 30, unicode: false),
                        Simbolo = c.String(nullable: false, maxLength: 4, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadoBono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistorialBono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BonoId = c.Int(nullable: false),
                        EstadoBonoId = c.Int(nullable: false),
                        FechaEntradaEstado = c.DateTime(nullable: false, precision: 0),
                        FechaSalidaEstado = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EstadoBono", t => t.EstadoBonoId)
                .ForeignKey("dbo.Bono", t => t.BonoId)
                .Index(t => t.BonoId)
                .Index(t => t.EstadoBonoId);
            
            CreateTable(
                "dbo.Tasa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonedaId = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        Fecha = c.DateTime(nullable: false, precision: 0),
                        Activa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moneda", t => t.MonedaId)
                .Index(t => t.MonedaId);
            
            CreateTable(
                "dbo.Moneda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 50, unicode: false),
                        Simbolo = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 200, unicode: false),
                        NombreUsuario = c.String(nullable: false, maxLength: 20, unicode: false),
                        Contrasena = c.String(nullable: false, maxLength: 20, unicode: false),
                        Activo = c.Boolean(nullable: false),
                        RolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rol", t => t.RolId)
                .Index(t => t.NombreUsuario, unique: true)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Usuario", "RolId", "dbo.Rol");
            DropForeignKey("dbo.Bono", "TasaId", "dbo.Tasa");
            DropForeignKey("dbo.Tasa", "MonedaId", "dbo.Moneda");
            DropForeignKey("dbo.HistorialBono", "BonoId", "dbo.Bono");
            DropForeignKey("dbo.HistorialBono", "EstadoBonoId", "dbo.EstadoBono");
            DropForeignKey("dbo.Bono", "EstadoBonoId", "dbo.EstadoBono");
            DropForeignKey("dbo.ListaCompra", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.ListaProducto", "ListaCompraId", "dbo.ListaCompra");
            DropForeignKey("dbo.ListaProducto", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.Producto", "MedidaId", "dbo.Medida");
            DropForeignKey("dbo.Producto", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Bono", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Usuario", new[] { "RolId" });
            DropIndex("dbo.Usuario", new[] { "NombreUsuario" });
            DropIndex("dbo.Tasa", new[] { "MonedaId" });
            DropIndex("dbo.HistorialBono", new[] { "EstadoBonoId" });
            DropIndex("dbo.HistorialBono", new[] { "BonoId" });
            DropIndex("dbo.Producto", new[] { "CategoriaId" });
            DropIndex("dbo.Producto", new[] { "MedidaId" });
            DropIndex("dbo.ListaProducto", new[] { "ProductoId" });
            DropIndex("dbo.ListaProducto", new[] { "ListaCompraId" });
            DropIndex("dbo.ListaCompra", new[] { "ClienteId" });
            DropIndex("dbo.Cliente", new[] { "Usuario" });
            DropIndex("dbo.Bono", new[] { "EstadoBonoId" });
            DropIndex("dbo.Bono", new[] { "TasaId" });
            DropIndex("dbo.Bono", new[] { "ClienteId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Usuario");
            DropTable("dbo.Rol");
            DropTable("dbo.Moneda");
            DropTable("dbo.Tasa");
            DropTable("dbo.HistorialBono");
            DropTable("dbo.EstadoBono");
            DropTable("dbo.Medida");
            DropTable("dbo.Categoria");
            DropTable("dbo.Producto");
            DropTable("dbo.ListaProducto");
            DropTable("dbo.ListaCompra");
            DropTable("dbo.Cliente");
            DropTable("dbo.Bono");
        }
    }
}
