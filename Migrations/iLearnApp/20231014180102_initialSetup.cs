using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iLearn.Migrations.iLearnApp
{
    public partial class initialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(type: "int", nullable: false),
                    EvaluationDesc = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.EvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorFirstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    InstructorLastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    InstructorDescription = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    InstructorEmailId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    InstructorExprerience = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    InstructorCertifications = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionDesc = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    QuestionAnswerOptionId = table.Column<short>(type: "smallint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    CourseTitle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CourseDescription = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    IsCertificationProvided = table.Column<bool>(type: "bit", nullable: true),
                    IsEvaluationRequired = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__FC00E001B39E4CF6", x => x.CourseCode);
                    table.ForeignKey(
                        name: "FK__Course__Instruct__1EA48E88",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "InstructorId");
                });

            migrationBuilder.CreateTable(
                name: "EvaluationQuestionMapping",
                columns: table => new
                {
                    EvaluationQuestionMappingId = table.Column<int>(type: "int", nullable: false),
                    EvaluationId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    CreatdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestionMapping", x => x.EvaluationQuestionMappingId);
                    table.ForeignKey(
                        name: "FK__Evaluatio__Evalu__1AD3FDA4",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId");
                    table.ForeignKey(
                        name: "FK__Evaluatio__Quest__1BC821DD",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptionsMapping",
                columns: table => new
                {
                    QuestionOptionsMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    OptionId = table.Column<short>(type: "smallint", nullable: true),
                    OptionDesc = table.Column<int>(type: "int", nullable: true),
                    CreatdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__5F596537A035B7F9", x => x.QuestionOptionsMapId);
                    table.ForeignKey(
                        name: "FK__QuestionO__Quest__160F4887",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "CourseSections",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    SectionTitle = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SectionDescription = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    SectionDuration = table.Column<int>(type: "int", nullable: true),
                    CourseContentPath = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    IsEvaluationMandotry = table.Column<bool>(type: "bit", nullable: true),
                    EvalutationId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseSe__80EF08723F362C42", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK__CourseSec__Cours__2180FB33",
                        column: x => x.CourseCode,
                        principalTable: "Course",
                        principalColumn: "CourseCode");
                    table.ForeignKey(
                        name: "FK__CourseSec__Evalu__22751F6C",
                        column: x => x.EvalutationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId");
                });

            migrationBuilder.CreateTable(
                name: "UserCourseMapping",
                columns: table => new
                {
                    UserCourseMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CourseCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    EnrolledOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentSectionId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseMapping", x => x.UserCourseMappingId);
                    table.ForeignKey(
                        name: "FK__UserCours__Cours__2645B050",
                        column: x => x.CourseCode,
                        principalTable: "Course",
                        principalColumn: "CourseCode");
                    table.ForeignKey(
                        name: "FK__UserCours__Curre__2739D489",
                        column: x => x.CurrentSectionId,
                        principalTable: "CourseSections",
                        principalColumn: "SectionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_InstructorId",
                table: "Course",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_CourseCode",
                table: "CourseSections",
                column: "CourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_EvalutationId",
                table: "CourseSections",
                column: "EvalutationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionMapping_EvaluationId",
                table: "EvaluationQuestionMapping",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionMapping_QuestionId",
                table: "EvaluationQuestionMapping",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptionsMapping_QuestionId",
                table: "QuestionOptionsMapping",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseMapping_CourseCode",
                table: "UserCourseMapping",
                column: "CourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseMapping_CurrentSectionId",
                table: "UserCourseMapping",
                column: "CurrentSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationQuestionMapping");

            migrationBuilder.DropTable(
                name: "QuestionOptionsMapping");

            migrationBuilder.DropTable(
                name: "UserCourseMapping");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "CourseSections");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.DropTable(
                name: "Instructor");
        }
    }
}
