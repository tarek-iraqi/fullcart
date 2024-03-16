using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullCart.Server.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "IsDeleted", "Password", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[,]
                {
                    { new Guid("027b165e-6e93-4362-a3f5-ec99d436d67c"), null, null, "ramy.saad@gmail.com", false, "$2a$11$Vd8SQG7x48dPeBDRSTMwN.emF7LppBES8UG.V6/nxqHgX07gHvsZe", "Customer", null, null, "ramy.saad" },
                    { new Guid("35af7d10-a704-4b86-b0bb-c9ad5824e290"), null, null, "ahmed.maher@gmail.com", false, "$2a$11$e1JZ94gUuyI4kWz422UGTOgUJzrsYfszvpu.w7AadhyqSJSu1PQv6", "Customer", null, null, "ahmed.maher" },
                    { new Guid("a658e7ff-509e-409c-94f4-774978ba3306"), null, null, "tarek.iraqi@gmail.com", false, "$2a$11$BiLqbYVEoR5.QqsgrbLs4etv7TQUK.Ym.GMIBXxTdVOwbosYHTBLS", "Admin", null, null, "tarek.iraqi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("027b165e-6e93-4362-a3f5-ec99d436d67c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35af7d10-a704-4b86-b0bb-c9ad5824e290"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a658e7ff-509e-409c-94f4-774978ba3306"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
