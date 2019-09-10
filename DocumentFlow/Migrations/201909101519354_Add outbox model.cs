namespace DocumentFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addoutboxmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutboxDocumentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DocIndex = c.String(),
                        Description = c.String(),
                        Author = c.String(),
                        LeadResolution = c.String(),
                        LeadResolutionLogin = c.String(),
                        SaveTime = c.Int(nullable: false),
                        DocumentFile = c.String(),
                        NoteToDocument = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OutboxDocumentModels");
        }
    }
}
