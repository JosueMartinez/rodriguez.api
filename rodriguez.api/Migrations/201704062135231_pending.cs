namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pending : DbMigration
    {
        public override void Up()
        {
            //DropIndex("tasamoneda", new[] { "monedaId" });
            //CreateIndex("tasamoneda", new[] { "monedaId", "activo" }, unique: true, name: "UQ_tasa");
        }
        
        public override void Down()
        {
            //DropIndex("tasamoneda", "UQ_tasa");
            //CreateIndex("tasamoneda", "monedaId");
        }
    }
}
