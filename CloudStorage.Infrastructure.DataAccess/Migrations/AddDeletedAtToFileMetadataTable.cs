using FluentMigrator;

namespace CloudStorage.Infrastructure.DataAccess.Migrations;

[Migration(2, TransactionBehavior.None)]
public class AddDeletedAtToFileMetadataTable : Migration
{
    public override void Up()
    {
        Alter.Table("file_metadata")
            .AddColumn("deleted_at").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Column("deleted_at").FromTable("file_metadata");
    }
}
