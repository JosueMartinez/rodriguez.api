namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasaActiva : DbMigration
    {
        public override void Up()
        {
            //AddColumn("tasamoneda", "activa", c => c.Boolean(nullable: false));
            //DropColumn("tasamoneda", "activo");
        }
        
        public override void Down()
        {
            //AddColumn("tasamoneda", "activo", c => c.Boolean(nullable: false));
            //DropColumn("tasamoneda", "activa");
        }
    }
}
