namespace Mi_Share.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner_ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 250),
                        Category_ID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Updated_By = c.Int(),
                        Updated_At = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Category_ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Requester_ID = c.Int(nullable: false),
                        Item_ID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Updated_By = c.Int(),
                        Updated_At = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.Item_ID)
                .ForeignKey("dbo.User", t => t.Requester_ID)
                .Index(t => t.Requester_ID)
                .Index(t => t.Item_ID);
            
            CreateTable(
                "dbo.Loan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Request_ID = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Request", t => t.Request_ID)
                .Index(t => t.Request_ID);
            
            CreateTable(
                "dbo.CollectionAccess",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner_ID = c.Int(nullable: false),
                        Requester_ID = c.Int(nullable: false),
                        DateRequested = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.Owner_ID)
                .ForeignKey("dbo.User", t => t.Requester_ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.Requester_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.CollectionAccess", "Requester_ID", "dbo.User");
            DropForeignKey("dbo.CollectionAccess", "Owner_ID", "dbo.User");
            DropForeignKey("dbo.Request", "Requester_ID", "dbo.User");
            DropForeignKey("dbo.Loan", "Request_ID", "dbo.Request");
            DropForeignKey("dbo.Request", "Item_ID", "dbo.Item");
            DropForeignKey("dbo.Item", "Category_ID", "dbo.Category");
            DropIndex("dbo.CollectionAccess", new[] { "Requester_ID" });
            DropIndex("dbo.CollectionAccess", new[] { "Owner_ID" });
            DropIndex("dbo.Loan", new[] { "Request_ID" });
            DropIndex("dbo.Request", new[] { "Item_ID" });
            DropIndex("dbo.Request", new[] { "Requester_ID" });
            DropIndex("dbo.Item", new[] { "Category_ID" });
            DropIndex("dbo.Item", new[] { "Owner_ID" });
            DropTable("dbo.CollectionAccess");
            DropTable("dbo.Loan");
            DropTable("dbo.Request");
            DropTable("dbo.User");
            DropTable("dbo.Item");
            DropTable("dbo.Category");
        }
    }
}
