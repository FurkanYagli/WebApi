namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sms", "GecerlilikTarih", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sms", "MesajSure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sms", "MesajSure", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sms", "GecerlilikTarih");
        }
    }
}
