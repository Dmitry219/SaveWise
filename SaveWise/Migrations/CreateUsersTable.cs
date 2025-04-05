using FluentMigrator;

namespace SaveWise.Migrations
{
    [Migration(202504042333)]
    public class CreateUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(30).NotNullable()
                .WithColumn("age").AsInt32().NotNullable()
                .WithColumn("dteCreate").AsDateTime().WithDefault(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            if (Schema.Table("Users").Exists())
            {
                Delete.Table("Users");
            }
        }
    }
}
