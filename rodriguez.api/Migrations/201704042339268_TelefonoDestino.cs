namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TelefonoDestino : DbMigration
    {
        public override void Up()
        {
            AddColumn("bono", "telefonoDestino", c => c.String(nullable: false, maxLength: 10, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("bono", "telefonoDestino");
        }
    }
}
