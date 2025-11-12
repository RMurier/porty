using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_CERTIFICATE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CERTIFICATE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_LANGUAGE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LANGUAGE", x => x.ID);
                    table.UniqueConstraint("AK_T_LANGUAGE_CODE", x => x.CODE);
                });

            migrationBuilder.CreateTable(
                name: "T_MAIL_TEMPLATE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MAIL_TEMPLATE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_PROJECT",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    URL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PROJECT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_ROLE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ROLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SCHOOL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SCHOOL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_CAREER",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    COMMENTS = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    REF_BUISNESS = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CAREER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_CAREER_T_BUISNESS_REF_BUISNESS",
                        column: x => x.REF_BUISNESS,
                        principalTable: "T_BUISNESS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_SKILL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NAME = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CERTIFICATION = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    REF_CATEGORY = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SKILL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_SKILL_T_CATEGORY_REF_CATEGORY",
                        column: x => x.REF_CATEGORY,
                        principalTable: "T_CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_MAIL_TEMPLATE_TRANSLATION",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LOCALE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SUBJECT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HTML_BODY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    REF_TEMPLATE = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MAIL_TEMPLATE_TRANSLATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_MAIL_TEMPLATE_TRANSLATION_T_LANGUAGE_LOCALE",
                        column: x => x.LOCALE,
                        principalTable: "T_LANGUAGE",
                        principalColumn: "CODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_MAIL_TEMPLATE_TRANSLATION_T_MAIL_TEMPLATE_REF_TEMPLATE",
                        column: x => x.REF_TEMPLATE,
                        principalTable: "T_MAIL_TEMPLATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_USER",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LASTNAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    SALT = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    REF_ROLE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    IS_EMAIL_VALIDATED = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    TOKEN_ACCOUNT_CREATED = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_USER_refRole__T_ROLE_id",
                        column: x => x.REF_ROLE,
                        principalTable: "T_ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_STUDY",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    COMMENTS = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    REF_SCHOOL = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_STUDY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_STUDY_T_SCHOOL_REF_SCHOOL",
                        column: x => x.REF_SCHOOL,
                        principalTable: "T_SCHOOL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_CAREER_SKILL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    REF_CAREER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    REF_SKILL = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CAREER_SKILL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_CAREER_SKILL_T_CAREER_REF_CAREER",
                        column: x => x.REF_CAREER,
                        principalTable: "T_CAREER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_CAREER_SKILL_T_SKILL_REF_SKILL",
                        column: x => x.REF_SKILL,
                        principalTable: "T_SKILL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_PROJECT_SKILL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    REF_PROJECT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    REF_SKILL = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PROJECT_SKILL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_PROJECT_SKILL_T_PROJECT_REF_PROJECT",
                        column: x => x.REF_PROJECT,
                        principalTable: "T_PROJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_PROJECT_SKILL_T_SKILL_REF_SKILL",
                        column: x => x.REF_SKILL,
                        principalTable: "T_SKILL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "T_LANGUAGE",
                columns: new[] { "ID", "CODE", "NAME" },
                values: new object[,]
                {
                    { 1, "fr", "Français" },
                    { 2, "en", "English" }
                });

            migrationBuilder.InsertData(
                table: "T_MAIL_TEMPLATE",
                columns: new[] { "ID", "DESCRIPTION", "IS_ACTIVE", "NAME" },
                values: new object[] { new Guid("7961e375-87e8-417a-8c67-5717c31d84f1"), "Mail envoyé lors de la création du compte avec un token pour valider le compte.", true, "ConfirmationInscription" });

            migrationBuilder.InsertData(
                table: "T_ROLE",
                columns: new[] { "ID", "DESCRIPTION", "NAME" },
                values: new object[,]
                {
                    { new Guid("74737d58-a69f-4df7-bf9a-777297a4d6d6"), "Utilisateur standard", "User" },
                    { new Guid("a3a52661-a5a1-4d13-a765-48543eb06cfe"), "Administrateur", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_CAREER_REF_BUISNESS",
                table: "T_CAREER",
                column: "REF_BUISNESS");

            migrationBuilder.CreateIndex(
                name: "IX_T_CAREER_SKILL_REF_CAREER",
                table: "T_CAREER_SKILL",
                column: "REF_CAREER");

            migrationBuilder.CreateIndex(
                name: "IX_T_CAREER_SKILL_REF_SKILL",
                table: "T_CAREER_SKILL",
                column: "REF_SKILL");

            migrationBuilder.CreateIndex(
                name: "IX_T_LANGUAGE_CODE",
                table: "T_LANGUAGE",
                column: "CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_MAIL_TEMPLATE_TRANSLATION_LOCALE",
                table: "T_MAIL_TEMPLATE_TRANSLATION",
                column: "LOCALE");

            migrationBuilder.CreateIndex(
                name: "IX_T_MAIL_TEMPLATE_TRANSLATION_REF_TEMPLATE_LOCALE",
                table: "T_MAIL_TEMPLATE_TRANSLATION",
                columns: new[] { "REF_TEMPLATE", "LOCALE" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_PROJECT_SKILL_REF_PROJECT",
                table: "T_PROJECT_SKILL",
                column: "REF_PROJECT");

            migrationBuilder.CreateIndex(
                name: "IX_T_PROJECT_SKILL_REF_SKILL",
                table: "T_PROJECT_SKILL",
                column: "REF_SKILL");

            migrationBuilder.CreateIndex(
                name: "IX_T_ROLE_NAME",
                table: "T_ROLE",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_SKILL_REF_CATEGORY",
                table: "T_SKILL",
                column: "REF_CATEGORY");

            migrationBuilder.CreateIndex(
                name: "IX_T_STUDY_REF_SCHOOL",
                table: "T_STUDY",
                column: "REF_SCHOOL");

            migrationBuilder.CreateIndex(
                name: "IX_T_USER_EMAIL",
                table: "T_USER",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_USER_REF_ROLE",
                table: "T_USER",
                column: "REF_ROLE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_CAREER_SKILL");

            migrationBuilder.DropTable(
                name: "T_CERTIFICATE");

            migrationBuilder.DropTable(
                name: "T_MAIL_TEMPLATE_TRANSLATION");

            migrationBuilder.DropTable(
                name: "T_PROJECT_SKILL");

            migrationBuilder.DropTable(
                name: "T_STUDY");

            migrationBuilder.DropTable(
                name: "T_USER");

            migrationBuilder.DropTable(
                name: "T_CAREER");

            migrationBuilder.DropTable(
                name: "T_LANGUAGE");

            migrationBuilder.DropTable(
                name: "T_MAIL_TEMPLATE");

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
