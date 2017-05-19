namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activo : DbMigration
    {
        public override void Up()
        {
            //AddColumn("smrodriguez.tasamoneda", "activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("smrodriguez.tasamoneda", "activo");
        }
    }
}
