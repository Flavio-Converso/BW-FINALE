using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team_5.Migrations
{
    /// <inheritdoc />
    public partial class Start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    IdBreed = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.IdBreed);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    IdExamination = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExaminationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.IdExamination);
                });

            migrationBuilder.CreateTable(
                name: "Hospitalizations",
                columns: table => new
                {
                    IdHospitalization = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHospitalized = table.Column<bool>(type: "bit", nullable: false),
                    HospDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitalizations", x => x.IdHospitalization);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    CF = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    UserIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.IdOwner);
                    table.ForeignKey(
                        name: "FK_Owners_Users_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsers",
                columns: table => new
                {
                    RolesIdRole = table.Column<int>(type: "int", nullable: false),
                    UsersIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsers", x => new { x.RolesIdRole, x.UsersIdUser });
                    table.ForeignKey(
                        name: "FK_RolesUsers_Roles_RolesIdRole",
                        column: x => x.RolesIdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesUsers_Users_UsersIdUser",
                        column: x => x.UsersIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    IdAnimal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumMicrochip = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OwnerIdOwner = table.Column<int>(type: "int", nullable: true),
                    HospitalizationIdHospitalization = table.Column<int>(type: "int", nullable: true),
                    ExaminationIdExamination = table.Column<int>(type: "int", nullable: true),
                    BreedIdBreed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.IdAnimal);
                    table.ForeignKey(
                        name: "FK_Animals_Breeds_BreedIdBreed",
                        column: x => x.BreedIdBreed,
                        principalTable: "Breeds",
                        principalColumn: "IdBreed",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animals_Examinations_ExaminationIdExamination",
                        column: x => x.ExaminationIdExamination,
                        principalTable: "Examinations",
                        principalColumn: "IdExamination");
                    table.ForeignKey(
                        name: "FK_Animals_Hospitalizations_HospitalizationIdHospitalization",
                        column: x => x.HospitalizationIdHospitalization,
                        principalTable: "Hospitalizations",
                        principalColumn: "IdHospitalization");
                    table.ForeignKey(
                        name: "FK_Animals_Owners_OwnerIdOwner",
                        column: x => x.OwnerIdOwner,
                        principalTable: "Owners",
                        principalColumn: "IdOwner");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_BreedIdBreed",
                table: "Animals",
                column: "BreedIdBreed");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ExaminationIdExamination",
                table: "Animals",
                column: "ExaminationIdExamination");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_HospitalizationIdHospitalization",
                table: "Animals",
                column: "HospitalizationIdHospitalization");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_OwnerIdOwner",
                table: "Animals",
                column: "OwnerIdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserIdUser",
                table: "Owners",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_UsersIdUser",
                table: "RolesUsers",
                column: "UsersIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "RolesUsers");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Hospitalizations");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
