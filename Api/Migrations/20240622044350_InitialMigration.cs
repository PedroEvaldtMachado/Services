using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    RegisterDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    PersonType = table.Column<int>(type: "integer", nullable: false),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryObligations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    PersonDetailType = table.Column<int>(type: "integer", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryObligations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryObligations_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contractee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorksCount = table.Column<int>(type: "integer", nullable: false),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractee_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contractor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestsCount = table.Column<int>(type: "integer", nullable: false),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractor_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    PersonDetailType = table.Column<int>(type: "integer", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDetail_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContracteeServiceProvide",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ContracteeId = table.Column<long>(type: "bigint", nullable: false),
                    Metric = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContracteeServiceProvide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContracteeServiceProvide_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingDate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContracteeId = table.Column<long>(type: "bigint", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulingDate_Contractee_ContracteeId",
                        column: x => x.ContracteeId,
                        principalTable: "Contractee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContracteeServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ContracteeId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<string>(type: "text", nullable: true),
                    ContractStatus = table.Column<int>(type: "integer", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Contractee_ContracteeId",
                        column: x => x.ContracteeId,
                        principalTable: "Contractee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContracteeServiceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContracteeServiceProvideId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceDetailType = table.Column<int>(type: "integer", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContracteeServiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContracteeServiceDetail_ContracteeServiceProvide_Contractee~",
                        column: x => x.ContracteeServiceProvideId,
                        principalTable: "ContracteeServiceProvide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StakeholderType = table.Column<int>(type: "integer", nullable: false),
                    ContractDetailType = table.Column<int>(type: "integer", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    ContractId = table.Column<long>(type: "bigint", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDetail_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Scheduling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContracteeId = table.Column<long>(type: "bigint", nullable: false),
                    ContractId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    EditControl = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scheduling_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Scheduling_Contractee_ContracteeId",
                        column: x => x.ContracteeId,
                        principalTable: "Contractee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContracteeId",
                table: "Contract",
                column: "ContracteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractorId",
                table: "Contract",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetail_ContractId",
                table: "ContractDetail",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractee_PersonId",
                table: "Contractee",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContracteeServiceDetail_ContracteeServiceProvideId",
                table: "ContracteeServiceDetail",
                column: "ContracteeServiceProvideId");

            migrationBuilder.CreateIndex(
                name: "IX_ContracteeServiceProvide_ServiceTypeId",
                table: "ContracteeServiceProvide",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractor_PersonId",
                table: "Contractor",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryObligations_CountryId",
                table: "CountryObligations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDetail_PersonId",
                table: "PersonDetail",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_ContractId",
                table: "Scheduling",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_ContracteeId",
                table: "Scheduling",
                column: "ContracteeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingDate_ContracteeId",
                table: "SchedulingDate",
                column: "ContracteeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDetail");

            migrationBuilder.DropTable(
                name: "ContracteeServiceDetail");

            migrationBuilder.DropTable(
                name: "CountryObligations");

            migrationBuilder.DropTable(
                name: "PersonDetail");

            migrationBuilder.DropTable(
                name: "Scheduling");

            migrationBuilder.DropTable(
                name: "SchedulingDate");

            migrationBuilder.DropTable(
                name: "ContracteeServiceProvide");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "Contractee");

            migrationBuilder.DropTable(
                name: "Contractor");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
