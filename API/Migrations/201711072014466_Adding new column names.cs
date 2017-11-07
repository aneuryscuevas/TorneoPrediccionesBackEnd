namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingnewcolumnnames : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Teams", "LogoT", c => c.String());
            //AddColumn("dbo.Groups", "LogoG", c => c.String());
            //AddColumn("dbo.Leagues", "LogoL", c => c.String());
            //AddColumn("dbo.Tournaments", "LogoTo", c => c.String());
            //DropColumn("dbo.Teams", "Logo");
            //DropColumn("dbo.Groups", "Logo");
            //DropColumn("dbo.Leagues", "Logo");
            //DropColumn("dbo.Tournaments", "Logo");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Tournaments", "Logo", c => c.String());
            //AddColumn("dbo.Leagues", "Logo", c => c.String());
            //AddColumn("dbo.Groups", "Logo", c => c.String());
            //AddColumn("dbo.Teams", "Logo", c => c.String());
            //DropColumn("dbo.Tournaments", "LogoTo");
            //DropColumn("dbo.Leagues", "LogoL");
            //DropColumn("dbo.Groups", "LogoG");
            //DropColumn("dbo.Teams", "LogoT");
        }
    }
}
