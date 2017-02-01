namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LCActiva : DbMigration
    {
        public override void Up()
        {
            AddColumn("lista_compra", "activa", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ista_compra", "activa");
        }
    }
}
