namespace Management.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Columns_AvatarandApiToken_TableUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
            AddColumn("dbo.AspNetUsers", "ApiToken", c => c.String());
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreateDate");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "ApiToken");
            DropColumn("dbo.AspNetUsers", "Avatar");
        }
    }
}
