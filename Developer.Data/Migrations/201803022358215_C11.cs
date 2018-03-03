namespace Developer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class C11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reminders", "ReminderNote", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reminders", "ReminderNote", c => c.String(nullable: false));
        }
    }
}
