using FluentMigrator;

namespace CloudStorage.NotificationService.Persistence.Migrations;

[Migration(1, TransactionBehavior.None)]
public class AddFilePublishedEventInboxTable : Migration
{
    public override void Up()
    {
        Create.Table("file_published_event_inbox")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("file_metadata_id").AsGuid().NotNullable()
            .WithColumn("received_at").AsDateTimeOffset().NotNullable()
            .WithColumn("processing_at").AsDateTimeOffset().Nullable()
            .WithColumn("processed_at").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Table("file_published_event_inbox");
    }
}
