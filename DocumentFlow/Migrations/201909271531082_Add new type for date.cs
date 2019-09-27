namespace DocumentFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnewtypefordate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IncomingDocumentModels", "SaveTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncomingDocumentModels", "SaveTime", c => c.Int(nullable: false));
        }
    }
}
