using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6146406F-C38F-437D-8E5A-35EA3E9512D0", null, "User", "USER" },
                    { "85DE64D6-DA14-4712-B6F2-FF6E2D1CA8E5", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "67C6FAB9-1BE5-4485-BA17-EDC680A63BE7", 0, "31a3c7ef-57cf-45c4-a41b-d53de688f0ee", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEM8irb61/jrr6Bfv98rcjiBZ03BN55BwjNM8SPUkvqBafVi8vSoKHqbSUVXYAxGK1g==", null, false, "544527db-409f-45a6-9d76-99a5ae66ce13", false, "user@bookstore.com" },
                    { "CF2435E8-D1EC-416B-9D24-4B9CADF361AF", 0, "62aad136-b16c-4754-b0c6-3840130d96e2", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEMkYRucV0lyZCbb+Tcyl3yS3gQm+D2U3jSrhSQR36dRyOO8qRkCpNjWnRifb7/WfJw==", null, false, "58424042-dfe3-4402-b843-1011d8e20a75", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6146406F-C38F-437D-8E5A-35EA3E9512D0", "67C6FAB9-1BE5-4485-BA17-EDC680A63BE7" },
                    { "85DE64D6-DA14-4712-B6F2-FF6E2D1CA8E5", "CF2435E8-D1EC-416B-9D24-4B9CADF361AF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6146406F-C38F-437D-8E5A-35EA3E9512D0", "67C6FAB9-1BE5-4485-BA17-EDC680A63BE7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "85DE64D6-DA14-4712-B6F2-FF6E2D1CA8E5", "CF2435E8-D1EC-416B-9D24-4B9CADF361AF" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6146406F-C38F-437D-8E5A-35EA3E9512D0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85DE64D6-DA14-4712-B6F2-FF6E2D1CA8E5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67C6FAB9-1BE5-4485-BA17-EDC680A63BE7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CF2435E8-D1EC-416B-9D24-4B9CADF361AF");
        }
    }
}
