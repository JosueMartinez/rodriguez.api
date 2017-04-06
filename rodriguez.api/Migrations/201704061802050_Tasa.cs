namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tasa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "tasamoneda",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        monedaId = c.Int(nullable: false),
                        valor = c.Double(nullable: false),
                        fecha = c.DateTime(nullable: false, precision: 0),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                //.ForeignKey("moneda", t => t.monedaId)
                .Index(t => new { t.monedaId, t.activo }, unique: true, name: "UQ_tasa"); ;

                AddForeignKey("tasamoneda", "monedaId", "moneda", "Id");

        }
        
        public override void Down()
        {
            DropForeignKey("tasamoneda", "monedaId", "moneda");
            DropIndex("tasamoneda", "UQ_tasa");
            DropTable("tasamoneda");
        }
    }
}
