using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class Adicionandocustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "c907131a-b02d-4383-82d9-50594ce0866b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "58a3bddb-0b35-4dfa-b332-12ce5c159daa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c515a72-d60f-41e5-91f8-3fe2e9940cb1", "AQAAAAEAACcQAAAAEJZkvg1vOe3/CWjm4oCRXuydHWXqO3nBWr34/ZKQ92RS+9kHnOiiW9ZjFCeeEGXtJg==", "e6454ba8-cb7e-43c7-8482-4280ac1aa5c7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "19e27e05-f492-4303-af55-5327afcab94d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "95b96e19-a553-43fe-abf9-a0414758474f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16d0da52-180e-443d-a2cc-a9ffc1db49eb", "AQAAAAEAACcQAAAAEA7t7HQsQ3HQhFD4iWC9V1EiWEixTssCphwWnHkSL+EaIQgXXQKaWctTFbyJWnod5g==", "55b09caa-b63c-41cb-bbf4-52a807903eae" });
        }
    }
}
