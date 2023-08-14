namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smsGuncelleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sms", "Kod", c => c.String());
            AddColumn("dbo.Sms", "GidenTel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sms", "GidenTel");
            DropColumn("dbo.Sms", "Kod");
        }
    }
}
