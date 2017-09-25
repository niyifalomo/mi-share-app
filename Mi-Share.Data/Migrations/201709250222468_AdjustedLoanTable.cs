namespace Mi_Share.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedLoanTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loan", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Loan", "ReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
