using FluentMigrator;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Migrations;

[Migration(3, TransactionBehavior.None)]
public sealed class AddFileMetadataDeletedOutboxTable : Migration
{
    public override void Up()
    {
        Create.Table("file_metadata_deleted_outbox")
            .WithColumn("event_id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("file_metadata_id").AsGuid().NotNullable()
            .WithColumn("outbox_status").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            ;
    }

    public override void Down()
    {
        Delete.Table("file_metadata_deleted_outbox");
    }
}
