namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.City", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.City", "Description");
        }
    }
}
