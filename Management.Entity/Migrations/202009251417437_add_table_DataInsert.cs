namespace Management.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_DataInsert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataInserts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataInsertTmps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TimeCreate = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataInsertTmps");
            DropTable("dbo.DataInserts");
        }
    }
}
