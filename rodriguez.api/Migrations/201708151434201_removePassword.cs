namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removePassword : DbMigration
    {
        public override void Up()
        {
            DropColumn("cliente", "Password");
        }
        
        public override void Down()
        {
            AddColumn("cliente", "Password", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
        }
    }
}
