using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class CreatingDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DedpartmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCompanies",
                columns: table => new
                {
                    InsuranceCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCompanyName = table.Column<string>(name: "Insurance Company Name", type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<double>(name: "Discount Percentage", type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompanies", x => x.InsuranceCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientFullName = table.Column<string>(name: "Patient Full Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patientsage = table.Column<int>(name: "Patient's age", type: "int", nullable: false),
                    Patientsphonenumber = table.Column<string>(name: "Patient's phone number", type: "nvarchar(max)", nullable: false),
                    PatientsEmial = table.Column<string>(name: "Patient's Emial", type: "nvarchar(max)", nullable: false),
                    CreatingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDataTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorFullName = table.Column<string>(name: "Doctor Full Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoctorsDegree = table.Column<string>(name: "Doctor's Degree", type: "nvarchar(max)", nullable: false),
                    DegreeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctorsdays = table.Column<string>(name: "Doctor's days", type: "nvarchar(max)", nullable: false),
                    Doctorstime = table.Column<DateTime>(name: "Doctor's time", type: "datetime2", nullable: false),
                    CheckUpFee = table.Column<double>(name: "Check Up Fee", type: "float", nullable: false),
                    AddotionalFee = table.Column<double>(name: "Addotional Fee", type: "float", nullable: false),
                    Doctorsphonenumber1 = table.Column<string>(name: "Doctor's phone number 1", type: "nvarchar(max)", nullable: false),
                    Doctorsphonenumber2 = table.Column<string>(name: "Doctor's phone number 2", type: "nvarchar(max)", nullable: false),
                    DoctorsEmail = table.Column<string>(name: "Doctor's Email", type: "nvarchar(max)", nullable: false),
                    DoctorBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDataTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DocId);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitingTime = table.Column<DateTime>(name: "Visiting Time", type: "datetime2", nullable: false),
                    Prescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitcheckUpFree = table.Column<double>(type: "float", nullable: false),
                    PatientID = table.Column<int>(name: "Patient ID", type: "int", nullable: false),
                    DoctorID = table.Column<int>(name: "Doctor ID", type: "int", nullable: false),
                    InsuranceCompany = table.Column<int>(name: "Insurance Company", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visits_Doctors_Doctor ID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visits_InsuranceCompanies_Insurance Company",
                        column: x => x.InsuranceCompany,
                        principalTable: "InsuranceCompanies",
                        principalColumn: "InsuranceCompanyId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visits_patients_Patient ID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Department ID",
                table: "Doctors",
                column: "Department ID");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Doctor ID",
                table: "Visits",
                column: "Doctor ID");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Insurance Company",
                table: "Visits",
                column: "Insurance Company");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Patient ID",
                table: "Visits",
                column: "Patient ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "InsuranceCompanies");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
