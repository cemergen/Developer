namespace Developer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class C1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        RemindDate = c.DateTime(nullable: false),
                        ReminderNote = c.String(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TaskItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskItems", t => t.TaskItem_Id)
                .Index(t => t.TaskItem_Id);
            
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 40),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ToDoItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToDoItems", t => t.ToDoItem_Id)
                .Index(t => t.ToDoItem_Id);
            
            CreateTable(
                "dbo.ToDoItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 40),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItems", "ToDoItem_Id", "dbo.ToDoItems");
            DropForeignKey("dbo.Reminders", "TaskItem_Id", "dbo.TaskItems");
            DropIndex("dbo.TaskItems", new[] { "ToDoItem_Id" });
            DropIndex("dbo.Reminders", new[] { "TaskItem_Id" });
            DropTable("dbo.ToDoItems");
            DropTable("dbo.TaskItems");
            DropTable("dbo.Reminders");
        }
    }
}
