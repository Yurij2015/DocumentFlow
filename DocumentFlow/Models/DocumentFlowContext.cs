using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DocumentFlow.Models
{
    public class DocumentFlowContext : ApplicationDbContext
    {
        public DbSet<IncomingDocumentModel> IncomingDocuments { get; set; }

        public System.Data.Entity.DbSet<DocumentFlow.Models.OutboxDocumentModel> OutboxDocumentModels { get; set; }
    }
}