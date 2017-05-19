namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unknown1 : DbMigration
    {
        public override void Up()
        {
            //DropColumn("tasamoneda", "activo");
        }
        
        public override void Down()
        {
            //AddColumn("tasamoneda", "activo", c => c.Boolean(nullable: false));
        }
    }
}
