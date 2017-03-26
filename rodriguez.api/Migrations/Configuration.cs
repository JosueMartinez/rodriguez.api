namespace rodriguez.api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<rodriguez.api.Models.RodriguezModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            // register mysql code generator
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(rodriguez.api.Models.RodriguezModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            #region rol
            context.rols.AddOrUpdate(
                   r => r.descripcion,
                   new Models.rol { descripcion = "Developer" },
                   new Models.rol { descripcion = "Admin" },
                   new Models.rol { descripcion = "Power User" },
                   new Models.rol { descripcion = "Empleado" }
                );
            #endregion
            #region medidas
            context.medidas.AddOrUpdate(
                    m => m.descripcion,
                    new Models.medida { descripcion = "Libra", simbolo = "lb" },
                    new Models.medida { descripcion = "Litro", simbolo = "lt" },
                    new Models.medida { descripcion = "Galón", simbolo = "gl" },
                    new Models.medida { descripcion = "Yarda", simbolo = "yd" },
                    new Models.medida { descripcion = "Onza", simbolo = "oz" }
                );
            #endregion

            #region categorias
            context.categorias.AddOrUpdate(
                    c => c.descripcion,
                    new Models.categoria { descripcion = "Ferreteria" },
                    new Models.categoria { descripcion = "Carniceria" },
                    new Models.categoria { descripcion = "Embutidos" },
                    new Models.categoria { descripcion = "Frutas" },
                    new Models.categoria { descripcion = "Verduras" },
                    new Models.categoria { descripcion = "Vegetales" },
                    new Models.categoria { descripcion = "Panaderia" },
                    new Models.categoria { descripcion = "Dulces" },
                    new Models.categoria { descripcion = "Huevos" },
                    new Models.categoria { descripcion = "Lacteos" },
                    new Models.categoria { descripcion = "Pastas" },
                    new Models.categoria { descripcion = "Aceites" },
                    new Models.categoria { descripcion = "Conservas" },
                    new Models.categoria { descripcion = "Comida Preparada" },
                    new Models.categoria { descripcion = "Zumos" },
                    new Models.categoria { descripcion = "Bebidas" },
                    new Models.categoria { descripcion = "Aperitivos" },
                    new Models.categoria { descripcion = "Infantil" },
                    new Models.categoria { descripcion = "Cosmeticos" },
                    new Models.categoria { descripcion = "Cuidado Personal" },
                    new Models.categoria { descripcion = "Hogar" },
                    new Models.categoria { descripcion = "Limpieza" }
                );
            #endregion

            #region monedas
            context.monedas.AddOrUpdate(
                   m => m.descripcion,
                   new Models.moneda { descripcion = "Peso Dominicano", simbolo = "RD" },
                   new Models.moneda { descripcion = "Dolar Estadounidense", simbolo = "USD" },
                   new Models.moneda { descripcion = "Euro", simbolo = "EUR" }

                );
            #endregion

            #region estados
            context.estadobonos.AddOrUpdate(
                    m => m.descripcion,
                    new Models.estadobono { descripcion = "Comprado" },
                    new Models.estadobono { descripcion = "Combrado" },
                    new Models.estadobono { descripcion = "Cancelado" }
                );
            #endregion
        }
    }
}
