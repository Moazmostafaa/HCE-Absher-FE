using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCE.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "__stub");

            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.EnsureSchema(
                name: "Audit");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "__stub_query_data",
                schema: "__stub",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    long1 = table.Column<long>(type: "bigint", nullable: true),
                    long2 = table.Column<long>(type: "bigint", nullable: true),
                    long3 = table.Column<long>(type: "bigint", nullable: true),
                    double1 = table.Column<double>(type: "float", nullable: true),
                    double2 = table.Column<double>(type: "float", nullable: true),
                    double3 = table.Column<double>(type: "float", nullable: true),
                    string1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    string2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    string3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    guid1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    guid2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    guid3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___stub_query_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuditChangedData",
                schema: "Audit",
                columns: table => new
                {
                    AuditDataChangesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SchemaName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentifierSaveChangesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditChangedData", x => x.AuditDataChangesId);
                });

            migrationBuilder.CreateTable(
                name: "AuditUserAction",
                schema: "Audit",
                columns: table => new
                {
                    AuditUserActionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditUserAction", x => x.AuditUserActionId);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                schema: "Identity",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Otp",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tries = table.Column<int>(type: "int", nullable: false),
                    TcnCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "General",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SizeByByte = table.Column<long>(type: "bigint", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachment_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "Identity",
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleModules",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleModulesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "Identity",
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleModules_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ProfileAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentificationAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BlockPeriod = table.Column<int>(type: "int", nullable: true),
                    BlockLawId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Attachment_IdentificationAttachmentId",
                        column: x => x.IdentificationAttachmentId,
                        principalSchema: "General",
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId");
                    table.ForeignKey(
                        name: "FK_User_Attachment_ProfileAttachmentId",
                        column: x => x.ProfileAttachmentId,
                        principalSchema: "General",
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Modules",
                columns: new[] { "ModuleId", "CreatedBy", "CreatedDate", "DeletedDate", "IsDeleted", "ModuleCode", "ModuleName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "2a4e1c24-aff9-41c2-b046-0f25613a3c1f", new DateTime(2021, 11, 9, 0, 43, 49, 480, DateTimeKind.Local), null, false, "Post", "Post", null, null });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "IsDeleted", "Name", "RoleId", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("bceb091d-6b80-4f37-ae10-8388a4172e8d"), "System", new DateTime(2021, 10, 27, 0, 43, 49, 480, DateTimeKind.Local), null, false, "SuperAdmin", new Guid("bceb091d-6b80-4f37-ae10-8388a4172e8d"), null, null });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "User",
                columns: new[] { "Id", "BlockLawId", "BlockPeriod", "CreatedBy", "CreatedDate", "DeletedDate", "Email", "Gender", "IdentificationAttachmentId", "IsActive", "IsDeleted", "Name", "NationalId", "Password", "PhoneNumber", "ProfileAttachmentId", "UpdatedBy", "UpdatedDate", "UserId", "UserName" },
                values: new object[] { new Guid("2a4e1c24-aff9-41c2-b046-0f25613a3c1f"), null, null, "System", new DateTime(2021, 10, 27, 0, 43, 49, 480, DateTimeKind.Local), null, "Super.admin@absher.com", 1, null, false, false, "Super Admin", null, "98ca5703dbd694f23e853efb870c6919c5947f1c8add29c96a11bf3c13a89c07", null, null, null, null, new Guid("2a4e1c24-aff9-41c2-b046-0f25613a3c1f"), "super.admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRole",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "IsDeleted", "RoleId", "UpdatedBy", "UpdatedDate", "UserId", "UserRoleId" },
                values: new object[] { new Guid("d867ac37-6e53-4880-89b6-fb867eb13c89"), "System", new DateTime(2021, 10, 27, 0, 43, 49, 480, DateTimeKind.Local), null, false, new Guid("bceb091d-6b80-4f37-ae10-8388a4172e8d"), null, null, new Guid("2a4e1c24-aff9-41c2-b046-0f25613a3c1f"), new Guid("d867ac37-6e53-4880-89b6-fb867eb13c89") });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_IsDeleted",
                schema: "General",
                table: "Attachment",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ModuleId",
                schema: "General",
                table: "Attachment",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditDataChangeType",
                schema: "Audit",
                table: "AuditChangedData",
                column: "ChangeType");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                schema: "Audit",
                table: "AuditChangedData",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreationDate",
                schema: "Audit",
                table: "AuditChangedData",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryKey",
                schema: "Audit",
                table: "AuditChangedData",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaName",
                schema: "Audit",
                table: "AuditChangedData",
                column: "SchemaName");

            migrationBuilder.CreateIndex(
                name: "IX_TableName",
                schema: "Audit",
                table: "AuditChangedData",
                column: "TableName");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                schema: "Audit",
                table: "AuditUserAction",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreationDate",
                schema: "Audit",
                table: "AuditUserAction",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IsDeleted",
                schema: "Identity",
                table: "Modules",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Otp_IsDeleted",
                schema: "Identity",
                table: "Otp",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Otp_NationalId",
                schema: "Identity",
                table: "Otp",
                column: "NationalId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Role_IsDeleted",
                schema: "Identity",
                table: "Role",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RoleModules_IsDeleted",
                schema: "Identity",
                table: "RoleModules",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RoleModules_ModuleId",
                schema: "Identity",
                table: "RoleModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleModules_RoleId",
                schema: "Identity",
                table: "RoleModules",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentificationAttachmentId",
                schema: "Identity",
                table: "User",
                column: "IdentificationAttachmentId",
                unique: true,
                filter: "[IdentificationAttachmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_IsDeleted",
                schema: "Identity",
                table: "User",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_User_NationalId",
                schema: "Identity",
                table: "User",
                column: "NationalId",
                unique: true,
                filter: "[NationalId] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfileAttachmentId",
                schema: "Identity",
                table: "User",
                column: "ProfileAttachmentId",
                unique: true,
                filter: "[ProfileAttachmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "Identity",
                table: "User",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_IsDeleted",
                schema: "Identity",
                table: "UserRole",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Identity",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "Identity",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_IsDeleted",
                schema: "Identity",
                table: "UserToken",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                schema: "Identity",
                table: "UserToken",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__stub_query_data",
                schema: "__stub");

            migrationBuilder.DropTable(
                name: "AuditChangedData",
                schema: "Audit");

            migrationBuilder.DropTable(
                name: "AuditUserAction",
                schema: "Audit");

            migrationBuilder.DropTable(
                name: "Otp",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleModules",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Attachment",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Modules",
                schema: "Identity");
        }
    }
}
