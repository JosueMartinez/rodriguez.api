namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productoLista : DbMigration
    {
        public override void Up()
        {
            CreateIndex("listacompraproducto", "productoId");
            AddForeignKey("listacompraproducto", "productoId", "producto", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("listacompraproducto", "productoId", "producto");
            DropIndex("listacompraproducto", new[] { "productoId" });
        }
    }
}
