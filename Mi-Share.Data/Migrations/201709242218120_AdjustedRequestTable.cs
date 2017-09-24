namespace Mi_Share.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedRequestTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Request", "Updated_By");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Request", "Updated_By", c => c.Int());
        }
    }
}
