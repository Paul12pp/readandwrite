namespace LeeryEscribir.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingEmpleados : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empleados", "Nombre", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empleados", "Nombre", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
