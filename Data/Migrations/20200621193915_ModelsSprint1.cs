using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ModelsSprint1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formations");

            migrationBuilder.CreateTable(
                name: "TrainingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TrainingCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quests_TrainingCourses_TrainingCourseId",
                        column: x => x.TrainingCourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourseOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    TrainingCourseId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourseOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCourseOwners_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingCourseOwners_TrainingCourses_TrainingCourseId",
                        column: x => x.TrainingCourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourseStudents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingCourseId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourseStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCourseStudents_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingCourseStudents_TrainingCourses_TrainingCourseId",
                        column: x => x.TrainingCourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourseTrainers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingCourseId = table.Column<int>(nullable: false),
                    TrainerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourseTrainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCourseTrainers_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingCourseTrainers_TrainingCourses_TrainingCourseId",
                        column: x => x.TrainingCourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    QuestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_TrainingCourseId",
                table: "Quests",
                column: "TrainingCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_QuestId",
                table: "Stages",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseOwners_OwnerId",
                table: "TrainingCourseOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseOwners_TrainingCourseId",
                table: "TrainingCourseOwners",
                column: "TrainingCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseStudents_StudentId",
                table: "TrainingCourseStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseStudents_TrainingCourseId",
                table: "TrainingCourseStudents",
                column: "TrainingCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseTrainers_TrainerId",
                table: "TrainingCourseTrainers",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseTrainers_TrainingCourseId",
                table: "TrainingCourseTrainers",
                column: "TrainingCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "TrainingCourseOwners");

            migrationBuilder.DropTable(
                name: "TrainingCourseStudents");

            migrationBuilder.DropTable(
                name: "TrainingCourseTrainers");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "TrainingCourses");

            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.Id);
                });
        }
    }
}
