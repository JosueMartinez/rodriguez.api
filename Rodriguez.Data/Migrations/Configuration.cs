namespace Rodriguez.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rodriguez.Data.Models.RodriguezModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            // register mysql code generator
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Rodriguez.Data.Models.RodriguezModel context)
        {
            #region Rol
            context.Roles.AddOrUpdate(
                   r => r.Descripcion,
                   new Models.Rol { Descripcion = "Developer" },
                   new Models.Rol { Descripcion = "Admin" },
                   new Models.Rol { Descripcion = "Power User" },
                   new Models.Rol { Descripcion = "Empleado" }
                );
            #endregion
            #region Medidas
            context.Medidas.AddOrUpdate(
                    m => m.Descripcion,
                    new Models.Medida { Descripcion = "Libra", Simbolo = "lb" },
                    new Models.Medida { Descripcion = "Litro", Simbolo = "lt" },
                    new Models.Medida { Descripcion = "Galón", Simbolo = "gl" },
                    new Models.Medida { Descripcion = "Yarda", Simbolo = "yd" },
                    new Models.Medida { Descripcion = "Onza", Simbolo = "oz" },
                    new Models.Medida { Descripcion = "Unidad", Simbolo = "ud" }
                );
            #endregion

            #region Categorias
            context.Categorias.AddOrUpdate(
                    c => c.Descripcion,
                    new Models.Categoria { Descripcion = "Ferreteria" },
                    new Models.Categoria { Descripcion = "Carniceria" },
                    new Models.Categoria { Descripcion = "Embutidos" },
                    new Models.Categoria { Descripcion = "Frutas" },
                    new Models.Categoria { Descripcion = "Verduras" },
                    new Models.Categoria { Descripcion = "Vegetales" },
                    new Models.Categoria { Descripcion = "Panaderia" },
                    new Models.Categoria { Descripcion = "Dulces" },
                    new Models.Categoria { Descripcion = "Huevos" },
                    new Models.Categoria { Descripcion = "Lacteos" },
                    new Models.Categoria { Descripcion = "Pastas" },
                    new Models.Categoria { Descripcion = "Aceites" },
                    new Models.Categoria { Descripcion = "Conservas" },
                    new Models.Categoria { Descripcion = "Comida Preparada" },
                    new Models.Categoria { Descripcion = "Zumos" },
                    new Models.Categoria { Descripcion = "Bebidas" },
                    new Models.Categoria { Descripcion = "Aperitivos" },
                    new Models.Categoria { Descripcion = "Infantil" },
                    new Models.Categoria { Descripcion = "Cosmeticos" },
                    new Models.Categoria { Descripcion = "Cuidado Personal" },
                    new Models.Categoria { Descripcion = "Hogar" },
                    new Models.Categoria { Descripcion = "Limpieza" }
                );
            #endregion


            #region Productos
            context.Productos.AddOrUpdate(
                p => p.Nombre,
                new Models.Producto
                {
                    Nombre = "Cerdo",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Carniceria")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Res",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Carniceria")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Chuleta",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Carniceria")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Salami",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Embutidos")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Jamon",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Embutidos")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Naranja",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Frutas")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("ud")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Sandia",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Frutas")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Lechuga",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Vegetales")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Tomate",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Vegetales")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Pan Sobao",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Panaderia")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("ud")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Leche",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Lacteos")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Ron",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Bebidas")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lt")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Cerveza",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Bebidas")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("lb")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Jabon",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Limpieza")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("ud")).FirstOrDefault().Id
                },
                new Models.Producto
                {
                    Nombre = "Papel Toalla",
                    CategoriaId = context.Categorias.Where(x => x.Descripcion.Equals("Limpieza")).FirstOrDefault().Id,
                    MedidaId = context.Medidas.Where(x => x.Simbolo.Equals("ud")).FirstOrDefault().Id
                }
            );
            #endregion  

            #region Monedas
            context.Monedas.AddOrUpdate(
                   m => m.Descripcion,
                   new Models.Moneda { Descripcion = "Peso Dominicano", Simbolo = "RD" },
                   new Models.Moneda { Descripcion = "Dolar Estadounidense", Simbolo = "USD" },
                   new Models.Moneda { Descripcion = "Euro", Simbolo = "EUR" }

                );
            #endregion

            #region estados
            context.EstadosBonos.AddOrUpdate(
                    m => m.Descripcion,
                    new Models.EstadoBono { Descripcion = "Comprado" },
                    new Models.EstadoBono { Descripcion = "Cobrado" },
                    new Models.EstadoBono { Descripcion = "Cancelado" }
                );
            #endregion

            #region Monedas
            context.Monedas.AddOrUpdate(
                m => m.Descripcion,
                new Models.Moneda { Descripcion = "PESO DOMINICANO", Simbolo = "RD" },
                new Models.Moneda { Descripcion = "DOLAR ESTADOUNIDENSE", Simbolo = "USD" },
                new Models.Moneda { Descripcion = "EURO", Simbolo = "EU" }
                );
            #endregion
        }
    }
}
