namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pending1 : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "bono", name: "tasamonedaId", newName: "tasaId");
            //RenameIndex(table: "bono", name: "IX_tasamonedaId", newName: "IX_tasaId");
        }
        
        public override void Down()
        {
            //RenameIndex(table: "bono", name: "IX_tasaId", newName: "IX_tasamonedaId");
            //RenameColumn(table: "bono", name: "tasaId", newName: "tasamonedaId");
        }
    }
}
