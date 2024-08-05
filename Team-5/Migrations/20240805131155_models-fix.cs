using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team_5.Migrations
{
    /// <inheritdoc />
    public partial class modelsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Examinations_ExaminationIdExamination",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Hospitalizations_HospitalizationIdHospitalization",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Owners_OwnerIdOwner",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ExaminationIdExamination",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_HospitalizationIdHospitalization",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ExaminationIdExamination",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "HospitalizationIdHospitalization",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "OwnerIdOwner",
                table: "Animals",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_OwnerIdOwner",
                table: "Animals",
                newName: "IX_Animals_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Hospitalizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Examinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CF",
                table: "Owners",
                column: "CF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_AnimalId",
                table: "Hospitalizations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_AnimalId",
                table: "Examinations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_Name",
                table: "Breeds",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Owners_OwnerId",
                table: "Animals",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "IdOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Animals_AnimalId",
                table: "Examinations",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "IdAnimal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Animals_AnimalId",
                table: "Hospitalizations",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "IdAnimal",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Owners_OwnerId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Animals_AnimalId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Animals_AnimalId",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Owners_CF",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_AnimalId",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_AnimalId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Breeds_Name",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Examinations");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Animals",
                newName: "OwnerIdOwner");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals",
                newName: "IX_Animals_OwnerIdOwner");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationIdExamination",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalizationIdHospitalization",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ExaminationIdExamination",
                table: "Animals",
                column: "ExaminationIdExamination");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_HospitalizationIdHospitalization",
                table: "Animals",
                column: "HospitalizationIdHospitalization");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Examinations_ExaminationIdExamination",
                table: "Animals",
                column: "ExaminationIdExamination",
                principalTable: "Examinations",
                principalColumn: "IdExamination");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Hospitalizations_HospitalizationIdHospitalization",
                table: "Animals",
                column: "HospitalizationIdHospitalization",
                principalTable: "Hospitalizations",
                principalColumn: "IdHospitalization");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Owners_OwnerIdOwner",
                table: "Animals",
                column: "OwnerIdOwner",
                principalTable: "Owners",
                principalColumn: "IdOwner");
        }
    }
}
