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
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
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

            context.SaveChanges();
        }
    }
}
