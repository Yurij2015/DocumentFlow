namespace DocumentFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelOutboxDoc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OutboxDocumentModels", "SaveTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OutboxDocumentModels", "SaveTime", c => c.Int(nullable: false));
        }
    }
}
