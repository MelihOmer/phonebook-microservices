using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    company = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contact_informations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_informations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contact_informations_contacts_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "Id", "company", "CreatedAt", "first_name", "IsDeleted", "last_name" },
                values: new object[,]
                {
                    { new Guid("321e8895-58c2-43ba-970b-0328d7177133"), "Company deleted", new DateTime(2025, 4, 23, 15, 32, 17, 90, DateTimeKind.Utc).AddTicks(8566), "Deleted", true, "Contact" },
                    { new Guid("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"), "Poseidon BT", new DateTime(2025, 4, 23, 15, 32, 17, 90, DateTimeKind.Utc).AddTicks(8559), "Melih Ömer", false, "KAMAR" },
                    { new Guid("ed584624-1260-493e-a206-3bb8b28f82a6"), "Company X", new DateTime(2025, 4, 23, 15, 32, 17, 90, DateTimeKind.Utc).AddTicks(8563), "Ali", false, "Veli" }
                });

            migrationBuilder.InsertData(
                table: "contact_informations",
                columns: new[] { "Id", "contact_id", "CreatedAt", "content", "IsDeleted", "type" },
                values: new object[,]
                {
                    { new Guid("1ae461cd-0c34-4999-aa6d-5d8a8553454e"), new Guid("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"), new DateTime(2025, 4, 23, 15, 32, 17, 91, DateTimeKind.Utc).AddTicks(8300), "Mersin", false, 3 },
                    { new Guid("2102795b-41dd-4d70-bb91-a321dac58656"), new Guid("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"), new DateTime(2025, 4, 23, 15, 32, 17, 91, DateTimeKind.Utc).AddTicks(8294), "0505 090 07 04", false, 1 },
                    { new Guid("2b97ff98-39c3-4cc3-a2ee-8fc5a596b818"), new Guid("ed584624-1260-493e-a206-3bb8b28f82a6"), new DateTime(2025, 4, 23, 15, 32, 17, 91, DateTimeKind.Utc).AddTicks(8306), "İstanbul", false, 3 },
                    { new Guid("46e06077-cd79-43f1-946b-9970b0e04557"), new Guid("b0e17976-0ee7-4662-8f75-9fb6ebd08a81"), new DateTime(2025, 4, 23, 15, 32, 17, 91, DateTimeKind.Utc).AddTicks(8297), "İstanbul", false, 3 },
                    { new Guid("efaba73d-fb91-4120-9863-dc693b1e61d1"), new Guid("ed584624-1260-493e-a206-3bb8b28f82a6"), new DateTime(2025, 4, 23, 15, 32, 17, 91, DateTimeKind.Utc).AddTicks(8303), "0505 505 05 05", false, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contact_informations_contact_id",
                table: "contact_informations",
                column: "contact_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact_informations");

            migrationBuilder.DropTable(
                name: "contacts");
        }
    }
}
