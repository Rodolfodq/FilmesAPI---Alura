using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class criandoroleregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "95b96e19-a553-43fe-abf9-a0414758474f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "19e27e05-f492-4303-af55-5327afcab94d", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16d0da52-180e-443d-a2cc-a9ffc1db49eb", "AQAAAAEAACcQAAAAEA7t7HQsQ3HQhFD4iWC9V1EiWEixTssCphwWnHkSL+EaIQgXXQKaWctTFbyJWnod5g==", "55b09caa-b63c-41cb-bbf4-52a807903eae" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "94f2db29-6d3c-48f3-ab0d-170b7ccea3f7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbf427ce-d700-42e9-a826-10498da02936", "AQAAAAEAACcQAAAAEDX3Dm5exZi6rwHZN4gUuJTE3UGhfzSAzDo1M3X9FcsMj31xdundrZgobjOfCYkHew==", "9e430018-7808-4cec-8279-81d211ef04bd" });
        }
    }
}
