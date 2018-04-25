namespace ElConvertidor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InputImage",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        ConversionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversion", t => t.ConversionId, cascadeDelete: true)
                .Index(t => t.ConversionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InputImage", "ConversionId", "dbo.Conversion");
            DropIndex("dbo.InputImage", new[] { "ConversionId" });
            DropTable("dbo.InputImage");
            DropTable("dbo.Conversion");
        }
    }
}
