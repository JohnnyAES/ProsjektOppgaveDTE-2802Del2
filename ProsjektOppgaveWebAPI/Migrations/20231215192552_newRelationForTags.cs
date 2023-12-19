using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsjektOppgaveWebAPI.Migrations
{
    public partial class newRelationForTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTagRelations");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71d12dd0-d81b-4b0e-b51b-1a9deb3a0527");

            migrationBuilder.CreateTable(
                name: "PostTagRelations",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTagRelations", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTagRelations_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTagRelations_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d868d2be-7b04-4c9a-8fe3-d0b243c54137", 0, "0f9bdfef-1d4b-4d1a-a443-018de8ca7718", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEAMzpAaIBND8BH+FTiN2RQv96ZKt0b6qZcsg/lLmZAsFc3LXBeqT6bRyMVwxbiCLeg==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_PostTagRelations_TagId",
                table: "PostTagRelations",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTagRelations");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d868d2be-7b04-4c9a-8fe3-d0b243c54137");

            migrationBuilder.CreateTable(
                name: "BlogTagRelations",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagRelations", x => new { x.BlogId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BlogTagRelations_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagRelations_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71d12dd0-d81b-4b0e-b51b-1a9deb3a0527", 0, "ee079c12-00e0-4ec2-85d8-e0b552720d88", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEPFAbfaWxWUEG60yse2wLNHmvofOtZHr+tEiIupTUsCHhX476jq3YWEPfjNdFSncUA==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagRelations_TagId",
                table: "BlogTagRelations",
                column: "TagId");
        }
    }
}
