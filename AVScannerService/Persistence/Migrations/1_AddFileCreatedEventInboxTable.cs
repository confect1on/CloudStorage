using FluentMigrator;

namespace AVScannerService.Persistence.Migrations;

[Migration(1, TransactionBehavior.None)]
public class AddFileCreatedEventInboxTable : Migration
{
    public override void Up()
    {
        Create.Table("file_created_event_inbox")
            .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("aggregate_id").AsGuid().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("processed_at").AsDateTimeOffset().Nullable()
            .WithColumn("temporary_storage_id").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("file_created_event_inbox");
    }
}
