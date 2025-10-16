using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_BUISNESS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BUISNESS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_CATEGORY",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CATEGORY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_CERTIFICATE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CERTIFICATE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_PROJECT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PROJECT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_ROLE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_SCHOOL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SCHOOL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_CAREER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    RefBuisness = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CAREER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_CAREER_T_BUISNESS_RefBuisness",
                        column: x => x.RefBuisness,
                        principalTable: "T_BUISNESS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_SKILL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    RefCategory = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SKILL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_SKILL_T_CATEGORY_RefCategory",
                        column: x => x.RefCategory,
                        principalTable: "T_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_USER",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    pepper = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    refRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    tokenAccountCreated = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USER", x => x.id);
                    table.ForeignKey(
                        name: "FK_T_USER_refRole__T_ROLE_id",
                        column: x => x.refRole,
                        principalTable: "T_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_STUDY",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    RefSchool = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_STUDY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_STUDY_T_SCHOOL_RefSchool",
                        column: x => x.RefSchool,
                        principalTable: "T_SCHOOL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_CAREER_SKILL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RefCareer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefSkill = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CAREER_SKILL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_CAREER_SKILL_T_CAREER_RefCareer",
                        column: x => x.RefCareer,
                        principalTable: "T_CAREER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_CAREER_SKILL_T_SKILL_RefSkill",
                        column: x => x.RefSkill,
                        principalTable: "T_SKILL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_PROJECT_SKILL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefSkill = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PROJECT_SKILL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_PROJECT_SKILL_T_PROJECT_RefProject",
                        column: x => x.RefProject,
                        principalTable: "T_PROJECT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_PROJECT_SKILL_T_SKILL_RefSkill",
                        column: x => x.RefSkill,
                        principalTable: "T_SKILL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "T_ROLE",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("74737d58-a69f-4df7-bf9a-777297a4d6d6"), "Utilisateur standard", "User" },
                    { new Guid("a3a52661-a5a1-4d13-a765-48543eb06cfe"), "Administrateur", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_CAREER_RefBuisness",
                table: "T_CAREER",
                column: "RefBuisness");

            migrationBuilder.CreateIndex(
                name: "IX_T_CAREER_SKILL_RefCareer",
                table: "T_CAREER_SKILL",
                column: "RefCareer");

            migrationBuilder.CreateIndex(
                name: "IX_T_CAREER_SKILL_RefSkill",
                table: "T_CAREER_SKILL",
                column: "RefSkill");

            migrationBuilder.CreateIndex(
                name: "IX_T_PROJECT_SKILL_RefProject",
                table: "T_PROJECT_SKILL",
                column: "RefProject");

            migrationBuilder.CreateIndex(
                name: "IX_T_PROJECT_SKILL_RefSkill",
                table: "T_PROJECT_SKILL",
                column: "RefSkill");

            migrationBuilder.CreateIndex(
                name: "IX_T_ROLE_Name",
                table: "T_ROLE",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_SKILL_RefCategory",
                table: "T_SKILL",
                column: "RefCategory");

            migrationBuilder.CreateIndex(
                name: "IX_T_STUDY_RefSchool",
                table: "T_STUDY",
                column: "RefSchool");

            migrationBuilder.CreateIndex(
                name: "IX_T_USER_email",
                table: "T_USER",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_USER_refRole",
                table: "T_USER",
                column: "refRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_CAREER_SKILL");

            migrationBuilder.DropTable(
                name: "T_CERTIFICATE");

            migrationBuilder.DropTable(
                name: "T_PROJECT_SKILL");

            migrationBuilder.DropTable(
                name: "T_STUDY");

            migrationBuilder.DropTable(
                name: "T_USER");

            migrationBuilder.DropTable(
                name: "T_CAREER");

            migrationBuilder.DropTable(
                name: "T_PROJECT");

            migrationBuilder.DropTable(
                name: "T_SKILL");

            migrationBuilder.DropTable(
                name: "T_SCHOOL");

            migrationBuilder.DropTable(
                name: "T_ROLE");

            migrationBuilder.DropTable(
                name: "T_BUISNESS");

            migrationBuilder.DropTable(
                name: "T_CATEGORY");
        }
    }
}
