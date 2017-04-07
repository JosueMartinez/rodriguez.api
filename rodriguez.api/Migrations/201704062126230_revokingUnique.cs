namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revokingUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("tasamoneda", "UQ_tasa");
            CreateIndex("tasamoneda", "monedaId");
        }
        
        public override void Down()
        {
            DropIndex("tasamoneda", new[] { "monedaId" });
            CreateIndex("tasamoneda", new[] { "monedaId", "activo" }, unique: true, name: "UQ_tasa");
        }
    }
}
