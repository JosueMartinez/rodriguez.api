namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueUsuario : DbMigration
    {
        public override void Up()
        {
            //CreateIndex("usuario", "nombreUsuario", unique: true);
            //DropColumn("usuario", "confirmarContrasena");
        }
        
        public override void Down()
        {
            //AddColumn("usuario", "confirmarContrasena", c => c.String(unicode: false));
            //DropIndex("usuario", new[] { "nombreUsuario" });
        }
    }
}
