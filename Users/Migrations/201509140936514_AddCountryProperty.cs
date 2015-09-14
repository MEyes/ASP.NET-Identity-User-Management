namespace Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
