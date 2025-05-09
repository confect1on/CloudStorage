using FluentMigrator;

namespace CloudStorage.Infrastructure.DataAccess.Migrations;

[Migration(1, TransactionBehavior.None)]
public class AddFileMetadataTable : Migration
{
    public override void Up()
    {
        Create.Table("file_metadata")
            .WithColumn("id").AsGuid().PrimaryKey("file_metadata_pk")
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("storage_id").AsString().Nullable()
            .WithColumn("file_name").AsString().NotNullable()
            .WithColumn("file_size_in_bytes").AsInt32().NotNullable()
            .WithColumn("mime_type").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("file_metadata");
    }
}
