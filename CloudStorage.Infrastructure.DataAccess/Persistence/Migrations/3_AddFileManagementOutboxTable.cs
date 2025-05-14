using FluentMigrator;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Migrations;

[Migration(3, TransactionBehavior.None)]
public sealed class AddFileManagementOutboxTable : Migration
{
    public override void Up()
    {
        Create.Table("file_management_outbox")
            .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("type").AsFixedLengthString(255).NotNullable()
            .WithColumn("content").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("processed_at").AsDateTimeOffset().Nullable()
            .WithColumn("error_message").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("file_metadata_deleted_outbox");
    }
}
