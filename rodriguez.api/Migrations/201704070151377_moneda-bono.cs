namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class monedabono : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("bono", "monedaId", "moneda");
            //DropIndex("bono", new[] { "monedaId" });
            //DropColumn("bono", "monedaId");
        }
        
        public override void Down()
        {
            AddColumn("bono", "monedaId", c => c.Int(nullable: false));
            CreateIndex("bono", "monedaId");
            AddForeignKey("bono", "monedaId", "moneda", "id");
        }
    }
}
