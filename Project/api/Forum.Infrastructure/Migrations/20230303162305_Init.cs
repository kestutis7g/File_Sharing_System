using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    FileMime = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    FileBinary = table.Column<byte[]>(type: "varbinary(Max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Login = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileMime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Banned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Banned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Banned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupPost",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPost", x => new { x.GroupId, x.PostId });
                    table.ForeignKey(
                        name: "FK_GroupPost_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPost_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Banned", "CreatedAt", "Deleted", "Description", "Email", "FileMime", "Lastname", "Login", "ModifiedAt", "Name", "Password", "Phone", "ProfilePicture", "Role" },
                values: new object[] { new Guid("03c91358-6ed4-4a47-a8be-056c565e0684"), false, new DateTimeOffset(new DateTime(2023, 3, 3, 16, 23, 5, 371, DateTimeKind.Unspecified).AddTicks(577), new TimeSpan(0, 0, 0, 0, 0)), false, null, "admin@admin.com", null, "Admin", "Admin", new DateTimeOffset(new DateTime(2023, 3, 3, 16, 23, 5, 371, DateTimeKind.Unspecified).AddTicks(587), new TimeSpan(0, 0, 0, 0, 0)), "Admin", "Admin", null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Banned", "CreatedAt", "Deleted", "Description", "Email", "FileMime", "Lastname", "Login", "ModifiedAt", "Name", "Password", "Phone", "ProfilePicture", "Role" },
                values: new object[] { new Guid("7587cf30-c562-4a83-9588-7164a998516b"), false, new DateTimeOffset(new DateTime(2023, 3, 3, 16, 23, 5, 371, DateTimeKind.Unspecified).AddTicks(630), new TimeSpan(0, 0, 0, 0, 0)), false, null, "sigitas@gmail.com", null, "Sigitavicius", "Sigitas", new DateTimeOffset(new DateTime(2023, 3, 3, 16, 23, 5, 371, DateTimeKind.Unspecified).AddTicks(630), new TimeSpan(0, 0, 0, 0, 0)), "Sigitas", "Sigitas", null, null, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_UserId",
                table: "Group",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPost_PostId",
                table: "GroupPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserId",
                table: "GroupUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "GroupPost");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
