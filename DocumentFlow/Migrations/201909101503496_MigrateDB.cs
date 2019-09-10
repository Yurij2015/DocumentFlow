namespace DocumentFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncomingDocumentModels", "LeadResolutionLogin", c => c.String());
            AddColumn("dbo.IncomingDocumentModels", "NoteToDocument", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncomingDocumentModels", "NoteToDocument");
            DropColumn("dbo.IncomingDocumentModels", "LeadResolutionLogin");
        }
    }
}
