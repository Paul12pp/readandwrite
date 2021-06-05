namespace LeeryEscribir.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pendingchanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cedula = c.String(nullable: false, maxLength: 20, unicode: false),
                        Nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sueldo = c.Decimal(nullable: false, precision: 10, scale: 2, storeType: "numeric"),
                        Moneda = c.String(nullable: false, maxLength: 6, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Empleados");
        }
    }
}
