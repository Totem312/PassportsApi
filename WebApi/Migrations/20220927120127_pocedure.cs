﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

#nullable disable

namespace WebApi.Migrations
{
    public partial class pocedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createTableProc = Path.Combine("SQL", "CreateTempPass.sql");
            migrationBuilder.Sql(File.ReadAllText(createTableProc));

            var copyProc = Path.Combine("SQL", "CopyScript.sql");
            migrationBuilder.Sql(File.ReadAllText(copyProc));

            var validataProc = Path.Combine("SQL", "ValidateDataScript.sql");
            migrationBuilder.Sql(File.ReadAllText(validataProc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            var createProcSql = "DROP procedure tempPass ";
            migrationBuilder.Sql(createProcSql);

            var copyProcDrop = "DROP procedure load_passports ";
            migrationBuilder.Sql(copyProcDrop);
            var ValidataProcDrop = "DROP procedure validdata ";
            migrationBuilder.Sql(ValidataProcDrop);
        }
    }
}