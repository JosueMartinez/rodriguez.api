namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paypalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("bono", "paypalId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("bono", "paypalId");
        }
    }
}
