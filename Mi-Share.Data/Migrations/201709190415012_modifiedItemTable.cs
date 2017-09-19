namespace Mi_Share.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedItemTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Item", "DateCreated", c => c.DateTime());
        }
    }
}
