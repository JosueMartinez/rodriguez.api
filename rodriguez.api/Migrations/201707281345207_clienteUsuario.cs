namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clienteUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("cliente", "usuario", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
            AddColumn("cliente", "nombreCompleto", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AddColumn("cliente", "Password", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
            AddColumn("usuario", "nombreCompleto", c => c.String(nullable: false, maxLength: 200, unicode: false));
            CreateIndex("cliente", "usuario", unique: true);
            DropColumn("cliente", "nombres");
            DropColumn("cliente", "apellidos");
            DropColumn("usuario", "nombres");
            DropColumn("usuario", "apellidos");
        }
        
        public override void Down()
        {
            AddColumn("usuario", "apellidos", c => c.String(nullable: false, maxLength: 80, unicode: false));
            AddColumn("usuario", "nombres", c => c.String(nullable: false, maxLength: 40, unicode: false));
            AddColumn("cliente", "apellidos", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("cliente", "nombres", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropIndex("cliente", new[] { "usuario" });
            DropColumn("usuario", "nombreCompleto");
            DropColumn("cliente", "Password");
            DropColumn("cliente", "nombreCompleto");
            DropColumn("cliente", "usuario");
        }
    }
}
