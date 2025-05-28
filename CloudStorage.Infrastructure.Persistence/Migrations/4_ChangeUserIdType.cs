using FluentMigrator;

namespace CloudStorage.Infrastructure.Persistence.Persistence.Migrations;

[Migration(4, TransactionBehavior.None)]
public class ChangeUserIdType : Migration
{
    public override void Up()
    {
        Alter.Table("file_metadata")
            .AddColumn("owner_user_id").AsGuid().NotNullable();
        Delete.Column("user_id").FromTable("file_metadata");
    }

    public override void Down()
    {
        Alter.Table("file_metadata")
            .AddColumn("user_id").AsInt64().NotNullable();
        Delete.Column("owner_user_id").FromTable("file_metadata");
    }
}
